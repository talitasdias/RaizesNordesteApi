using RaizesNordeste.API.Domain.Entities;

namespace RaizesNordeste.API.Domain.Interfaces
{
    public interface IEstoqueRepository
    {
        Task<(IEnumerable<Estoque>, int)> GetAllAsync(int pagina, int tamanhoPagina);

        Task<Estoque?> GetByIdAsync(int id);

        Task<Estoque?> GetByProdutoAndUnidadeAsync(int produtoId, int unidadeId);

        Task<IEnumerable<Estoque>> GetByIdUnidadeAsync(int unidadeId);

        Task<Estoque> CreateAsync(Estoque estoque);

        Task UpdateAsync(Estoque estoque);

        Task<bool> ExistsAsync(int produtoId, int unidadeId);
    }
}