using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.DTOs.Pedido;
using RaizesNordeste.API.Application.Interfaces;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Enums;
using RaizesNordeste.API.Domain.Interfaces;
using RaizesNordeste.API.Infrastructure.Persistence;

namespace RaizesNordeste.API.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly AppDbContext _context;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoService(
            IPedidoRepository repository,
            IEstoqueRepository estoqueRepository,
            AppDbContext context,
            IProdutoRepository produtoRepository)
        {
            _repository = repository;
            _estoqueRepository = estoqueRepository;
            _context = context;
            _produtoRepository = produtoRepository;
        }

        public async Task<PedidoResponseDTO> CreateAsync(int usuarioId, PedidoCreateDTO dto)
        {
            decimal valorTotal = 0;

            var itensPedido = new List<ItemPedido>();

            foreach (var item in dto.Itens)
            {
                var produto = await _produtoRepository.GetByIdAsync(item.ProdutoId);

                if (produto == null)
                    throw new KeyNotFoundException($"Produto de Id {item.ProdutoId} não encontrado.");
                
                var estoque = await _estoqueRepository.GetByProdutoAndUnidadeAsync(item.ProdutoId, dto.UnidadeId);

                if (estoque == null)
                    throw new Exception(
                        $"Produto {produto.Nome} sem estoque.");

                if (estoque.Quantidade < item.Quantidade)
                    throw new InvalidOperationException("Estoque insuficiente para realizar o pedido.");

                estoque.Quantidade -= item.Quantidade;

                var subtotal = produto.Preco * item.Quantidade;

                valorTotal += subtotal;

                itensPedido.Add(new ItemPedido
                {
                    ProdutoId = produto.Id,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = produto.Preco,
                    Subtotal = subtotal
                });
            }

            await _context.SaveChangesAsync();

            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                UnidadeId = dto.UnidadeId,
                CanalPedido = (CanalPedido)dto.CanalPedido,
                Observacao = dto.Observacao,
                ValorTotal = valorTotal,
                StatusPedido = StatusPedido.AguardandoPagamento,
                Itens = itensPedido
            };

            await _repository.CreateAsync(pedido);

            return new PedidoResponseDTO
            {
                Id = pedido.Id,
                ValorTotal = pedido.ValorTotal,
                StatusPedido = pedido.StatusPedido.ToString(),
                CanalPedido = pedido.CanalPedido.ToString(),
                DataCriacao = pedido.DataCriacao
            };
        }

        public async Task<PaginacaoResponseDTO<PedidoResponseDTO>> GetByUsuarioIdAsync(
            int usuarioId,
            int pagina,
            int tamanhoPagina,
            CanalPedido? canalPedido,
            StatusPedido? statusPedido)
        {
            var (pedidos, total) = await _repository.GetByUsuarioIdAsync(
                usuarioId, pagina, tamanhoPagina, canalPedido, statusPedido);

            var itens = pedidos.Select(p => new PedidoResponseDTO
            {
                Id = p.Id,
                ValorTotal = p.ValorTotal,
                StatusPedido = p.StatusPedido.ToString(),
                CanalPedido = p.CanalPedido.ToString(),
                DataCriacao = p.DataCriacao,

                Itens = p.Itens.Select(i => new PedidoItemResponseDTO
                {
                    Produto = i.Produto.Nome,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    Subtotal = i.Subtotal
                }).ToList()

            }).ToList();

            return new PaginacaoResponseDTO<PedidoResponseDTO>
            {
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalItens = total,
                TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
                Itens = itens
            };
        }

        public async Task<bool> UpdateStatusAsync(int pedidoId, PedidoUpdateStatusDTO dto)
        {
            var pedido = await _repository.GetByIdAsync(pedidoId);

            if (pedido == null)
                return false;

            var novoStatus = (StatusPedido)dto.Status;

            if (!StatusTransitionIsValid(
                pedido.StatusPedido,
                novoStatus))
            {
                throw new Exception(
                    $"Transição inválida de status: " +
                    $"{pedido.StatusPedido} → {novoStatus}");
            }

            pedido.StatusPedido = novoStatus;

            await _repository.UpdateAsync(pedido);

            return true;
        }

        public async Task<bool> CancelAsync(int pedidoId)
        {
            var pedido = await _repository
                .GetDetailsByIdAsync(pedidoId);

            if (pedido == null)
                return false;

            if (
                pedido.StatusPedido == StatusPedido.EmPreparo ||
                pedido.StatusPedido == StatusPedido.Pronto ||
                pedido.StatusPedido == StatusPedido.Entregue ||
                pedido.StatusPedido == StatusPedido.Cancelado
            )
            {
                throw new Exception(
                    "Pedido não pode mais ser cancelado.");
            }

            foreach (var item in pedido.Itens)
            {
                var estoque = await _estoqueRepository.GetByProdutoAndUnidadeAsync(item.ProdutoId, pedido.UnidadeId);
                
                if (estoque != null)
                {
                    estoque.Quantidade += item.Quantidade;
                }
            }

            pedido.StatusPedido = StatusPedido.Cancelado;

            await _repository.UpdateAsync(pedido);

            await _context.SaveChangesAsync();

            return true;
        }

        private bool StatusTransitionIsValid(StatusPedido atual, StatusPedido novo)
        {
            return (atual, novo) switch
            {
                (StatusPedido.AguardandoPagamento, StatusPedido.Pago) => true,

                (StatusPedido.Pago, StatusPedido.EmPreparo) => true,

                (StatusPedido.EmPreparo, StatusPedido.Pronto) => true,

                (StatusPedido.Pronto, StatusPedido.Entregue) => true,

                _ => false
            };
        }
    }
}