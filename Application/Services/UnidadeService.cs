using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.DTOs.Unidade;
using RaizesNordeste.API.Application.Interfaces;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Interfaces;

namespace RaizesNordeste.API.Application.Services
{
    public class UnidadeService : IUnidadeService
    {
        private readonly IUnidadeRepository _repository;
        private readonly IEstoqueRepository _estoqueRepository;

        public UnidadeService(IUnidadeRepository repository, IEstoqueRepository estoqueRepository)
        {
            _repository = repository;
            _estoqueRepository = estoqueRepository;
        }

        public async Task<UnidadeResponseDTO> Create(UnidadeCreateDTO unidadeDto)
        {
            var unidade = new Unidade
            {
                Nome = unidadeDto.Nome,
                Endereco = unidadeDto.Endereco,
                Ativa = unidadeDto.Ativa
            };

            var unidadeCriada = await _repository.Create(unidade);

            return new UnidadeResponseDTO
            {
                Id = unidadeCriada.Id,
                Nome = unidadeCriada.Nome,
                Endereco = unidadeCriada.Endereco,
                Ativa = unidadeCriada.Ativa
            };
        }

        public async Task<PaginacaoResponseDTO<UnidadeResponseDTO>> GetAllAsync(int pagina, int tamanhoPagina)
        {
            var (unidades, total) = await _repository.GetAllAsync(pagina, tamanhoPagina);

            var itens = unidades.Select(x => new UnidadeResponseDTO
            {
                Id = x.Id,
                Nome = x.Nome,
                Endereco = x.Endereco,
                Ativa = x.Ativa
            }).ToList();

            return new PaginacaoResponseDTO<UnidadeResponseDTO>
            {
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalItens = total,
                TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
                Itens = itens
            };
        }

        public async Task<UnidadeResponseDTO> GetById(int id)
        {
            var unidade = await _repository.GetById(id);
            if (unidade is null)
                throw new KeyNotFoundException($"Unidade de Id {id} não encontrada.");
            
            return new UnidadeResponseDTO
            {
                Id = unidade.Id,
                Nome = unidade.Nome,
                Endereco = unidade.Endereco,
                Ativa = unidade.Ativa
            };
        }

        public async Task<List<CardapioItemDTO>> GetCardapioAsync(int unidadeId)
        {
            var unidade = await _repository.ExistsAsync(unidadeId);
            if (!unidade)
                throw new KeyNotFoundException($"Unidade de Id {unidadeId} não encontrada.");

            var estoques = await _estoqueRepository.GetByIdUnidadeAsync(unidadeId);

            return estoques.Where(x => x.Quantidade > 0)
                    .Select(x => new CardapioItemDTO
                    {
                        ProdutoId = x.Produto.Id,
                        Nome = x.Produto.Nome,
                        Descricao = x.Produto.Descricao,
                        Preco = x.Produto.Preco,
                        QuantidadeDisponivel = x.Quantidade
                    }).ToList();
        }
    }
}