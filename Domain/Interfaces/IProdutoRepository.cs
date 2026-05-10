using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Domain.Entities;

namespace RaizesNordeste.API.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<(IEnumerable<Produto>, int)> GetAllAsync(int pagina, int tamanhoPagina);
        Task<Produto?> GetByIdAsync(int id);
        Task<Produto> CreateAsync(ProdutoCreateDTO produtoDto);
        Task<Produto?> UpdateAsync(ProdutoUpdateDTO produtoDto, int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}