using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.DTOs.Estoque;
using RaizesNordeste.API.Application.Interfaces;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Interfaces;

namespace RaizesNordeste.API.Application.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _repository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IEstoqueRepository repository, IUnidadeRepository unidadeRepository, IProdutoRepository produtoRepository)
        {
            _repository = repository;
            _unidadeRepository = unidadeRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<PaginacaoResponseDTO<EstoqueResponseDTO>> GetAllAsync(int pagina, int tamanhoPagina)
        {
            var (estoques, total) = await _repository.GetAllAsync(pagina, tamanhoPagina);

            var itens = estoques.Select(x => new EstoqueResponseDTO
            {
                Id = x.Id,
                Produto = x.Produto.Nome,
                Unidade = x.Unidade.Nome,
                Quantidade = x.Quantidade,
                DataCriacao = x.DataCriacao
            }).ToList();

            return new PaginacaoResponseDTO<EstoqueResponseDTO>
            {
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalItens = total,
                TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
                Itens = itens
            };
        }

        public async Task<EstoqueResponseDTO> GetByIdAsync(int id)
        {
            var estoque = await _repository.GetByIdAsync(id);

            if (estoque == null)
                throw new KeyNotFoundException($"Estoque de Id {id} não encontrado.");

            return new EstoqueResponseDTO
            {
                Id = estoque.Id,
                Produto = estoque.Produto.Nome,
                Unidade = estoque.Unidade.Nome,
                Quantidade = estoque.Quantidade,
                DataCriacao = estoque.DataCriacao
            };
        }

        public async Task<IEnumerable<EstoqueResponseDTO>> GetByIdUnidadeAsync(int unidadeId)
        {
            var unidadeExiste = await _unidadeRepository.ExistsAsync(unidadeId);

            if (!unidadeExiste)
                throw new KeyNotFoundException($"Unidade {unidadeId} não encontrada.");

            var estoque = await _repository.GetByIdUnidadeAsync(unidadeId);

            if (!estoque.Any())
                return [];

            return estoque.Select(x => new EstoqueResponseDTO
            {
                Id = x.Id,
                Produto = x.Produto.Nome,
                Unidade = x.Unidade.Nome,
                Quantidade = x.Quantidade,
                DataCriacao = x.DataCriacao
            }).ToList();
        }

        public async Task<EstoqueResponseDTO> CreateAsync(EstoqueCreateDTO dto)
        {
            var unidadeExiste = await _unidadeRepository.ExistsAsync(dto.UnidadeId);
            if (!unidadeExiste)
                throw new KeyNotFoundException($"Unidade de Id {dto.UnidadeId} não encontrada.");

            var produtoExiste = await _produtoRepository.ExistsAsync(dto.ProdutoId);
            if (!produtoExiste)
                throw new KeyNotFoundException($"Produto de Id {dto.ProdutoId} não encontrada.");
            
            var estoqueJaExiste = await _repository.ExistsAsync(dto.ProdutoId, dto.UnidadeId);
            if (estoqueJaExiste)
                throw new InvalidOperationException("Já existe um estoque para esse produto nessa unidade.");

            var estoque = new Estoque
            {
                ProdutoId = dto.ProdutoId,
                UnidadeId = dto.UnidadeId,
                Quantidade = dto.Quantidade
            };

            await _repository.CreateAsync(estoque);

            estoque = await _repository.GetByIdAsync(estoque.Id);

            return new EstoqueResponseDTO
            {
                Id = estoque!.Id,
                Produto = estoque.Produto.Nome,
                Unidade = estoque.Unidade.Nome,
                Quantidade = estoque.Quantidade,
                DataCriacao = estoque.DataCriacao
            };
        }

        public async Task<bool> UpdateQuantidadeAsync(int id, EstoqueUpdateQuantidadeDTO dto)
        {
            var estoque = await _repository.GetByIdAsync(id);

            if (estoque == null)
                throw new KeyNotFoundException($"Estoque de Id {id} não encontrado.");

            estoque.Quantidade = dto.Quantidade;

            await _repository.UpdateAsync(estoque);

            return true;
        }
    }
}