using RaizesNordeste.API.Application.DTOs.Estoque;

namespace RaizesNordeste.API.Application.Interfaces
{
    public interface IEstoqueService
    {
        Task<List<EstoqueResponseDTO>> GetAllAsync();

        Task<EstoqueResponseDTO> GetByIdAsync(int id);

        Task<IEnumerable<EstoqueResponseDTO>> GetByIdUnidadeAsync(int unidadeId);

        Task<EstoqueResponseDTO> CreateAsync(EstoqueCreateDTO dto);

        Task<bool> UpdateQuantidadeAsync(int id, EstoqueUpdateQuantidadeDTO dto);
    }
}