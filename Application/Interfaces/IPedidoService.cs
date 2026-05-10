using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.DTOs.Pedido;
using RaizesNordeste.API.Domain.Enums;

namespace RaizesNordeste.API.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoResponseDTO> CreateAsync(int usuarioId, PedidoCreateDTO dto);

        Task<PaginacaoResponseDTO<PedidoResponseDTO>> GetByUsuarioIdAsync(
            int usuarioId,
            int pagina,
            int tamanhoPagina,
            CanalPedido? canalPedido,
            StatusPedido? statusPedido);

        Task<bool> UpdateStatusAsync(int pedidoId, PedidoUpdateStatusDTO dto);

        Task<bool> CancelAsync(int pedidoId);
    }
}