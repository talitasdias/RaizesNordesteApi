using RaizesNordeste.API.Application.DTOs;

namespace RaizesNordeste.API.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoResponseDTO>> GetAllAsync();
        Task<ProdutoResponseDTO?> GetByIdAsync(int id);
        Task<ProdutoCreateResponseDTO> CreateAsync(ProdutoCreateDTO produtoDto);
        Task<ProdutoResponseDTO?> UpdateAsync(ProdutoUpdateDTO produtoDto, int id);
        Task<bool> DeleteAsync(int id);
    }
}