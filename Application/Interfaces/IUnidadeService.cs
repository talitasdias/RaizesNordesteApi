using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.DTOs.Unidade;

namespace RaizesNordeste.API.Application.Interfaces
{
    public interface IUnidadeService
    {
        Task<IEnumerable<UnidadeResponseDTO>> GetAllAsync();
        Task<UnidadeResponseDTO> GetById(int id);
        Task<UnidadeResponseDTO> Create(UnidadeCreateDTO unidade);
        Task<List<CardapioItemDTO>> GetCardapioAsync(int unidadeId);
    }
}