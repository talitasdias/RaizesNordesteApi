using RaizesNordeste.API.Application.DTOs.Pagamento;

namespace RaizesNordeste.API.Application.Interfaces
{
    public interface IPagamentoService
    {
        Task<PagamentoResponseDTO> ProcessarPagamentoAsync(PagamentoCreateDTO dto);
    }
}