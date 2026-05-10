using RaizesNordeste.API.Application.DTOs.Pedido;

namespace RaizesNordeste.API.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoResponseDTO> CreateAsync(int usuarioId, PedidoCreateDTO dto);

        Task<List<PedidoResponseDTO>> GetByUsuarioIdAsync(int usuarioId);

        Task<bool> UpdateStatusAsync(int pedidoId, PedidoUpdateStatusDTO dto);

        Task<bool> CancelAsync(int pedidoId);
    }
}