using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.DTOs.Unidade;

namespace RaizesNordeste.API.Application.Interfaces
{
    public interface IUnidadeService
    {
        Task<PaginacaoResponseDTO<UnidadeResponseDTO>> GetAllAsync(int pagina, int tamanhoPagina);
        Task<UnidadeResponseDTO> GetById(int id);
        Task<UnidadeResponseDTO> Create(UnidadeCreateDTO unidade);
        Task<List<CardapioItemDTO>> GetCardapioAsync(int unidadeId);
    }
}