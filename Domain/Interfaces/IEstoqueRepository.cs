using RaizesNordeste.API.Domain.Entities;

namespace RaizesNordeste.API.Domain.Interfaces
{
    public interface IEstoqueRepository
    {
        Task<List<Estoque>> GetAllAsync();

        Task<Estoque?> GetByIdAsync(int id);

        Task<Estoque?> GetByProdutoAndUnidadeAsync(int produtoId, int unidadeId);

        Task<IEnumerable<Estoque>> GetByIdUnidadeAsync(int unidadeId);

        Task<Estoque> CreateAsync(Estoque estoque);

        Task UpdateAsync(Estoque estoque);
    }
}