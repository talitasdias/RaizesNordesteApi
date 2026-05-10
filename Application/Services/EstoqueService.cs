using RaizesNordeste.API.Application.DTOs.Estoque;
using RaizesNordeste.API.Application.Interfaces;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Interfaces;

namespace RaizesNordeste.API.Application.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _repository;

        public EstoqueService(IEstoqueRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EstoqueResponseDTO>> GetAllAsync()
        {
            var estoques = await _repository.GetAllAsync();

            return estoques.Select(x => new EstoqueResponseDTO
            {
                Id = x.Id,
                Produto = x.Produto.Nome,
                Unidade = x.Unidade.Nome,
                Quantidade = x.Quantidade,
                DataCriacao = x.DataCriacao
            }).ToList();
        }

        public async Task<EstoqueResponseDTO?> GetByIdAsync(int id)
        {
            var estoque = await _repository.GetByIdAsync(id);

            if (estoque == null)
                return null;

            return new EstoqueResponseDTO
            {
                Id = estoque.Id,
                Produto = estoque.Produto.Nome,
                Unidade = estoque.Unidade.Nome,
                Quantidade = estoque.Quantidade,
                DataCriacao = estoque.DataCriacao
            };
        }

        public async Task<EstoqueResponseDTO> CreateAsync(
            EstoqueCreateDTO dto)
        {
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

        public async Task<bool> UpdateQuantidadeAsync(
            int id,
            EstoqueUpdateQuantidadeDTO dto)
        {
            var estoque = await _repository.GetByIdAsync(id);

            if (estoque == null)
                return false;

            estoque.Quantidade = dto.Quantidade;

            await _repository.UpdateAsync(estoque);

            return true;
        }
    }
}