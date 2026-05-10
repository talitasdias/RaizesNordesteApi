using RaizesNordeste.API.Application.DTOs;

namespace RaizesNordeste.API.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<PaginacaoResponseDTO<ProdutoResponseDTO>> GetAllAsync(int pagina, int tamanhoPagina);
        Task<ProdutoResponseDTO?> GetByIdAsync(int id);
        Task<ProdutoCreateResponseDTO> CreateAsync(ProdutoCreateDTO produtoDto);
        Task<ProdutoResponseDTO?> UpdateAsync(ProdutoUpdateDTO produtoDto, int id);
        Task<bool> DeleteAsync(int id);
    }
}