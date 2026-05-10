using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Enums;

namespace RaizesNordeste.API.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> CreateAsync(Pedido pedido);

        Task<(List<Pedido>, int)> GetByUsuarioIdAsync(int usuarioId, int pagina, int tamanhoPagina,
            CanalPedido? canalPedido, StatusPedido? statusPedido);

        Task<Pedido?> GetByIdAsync(int id);

        Task<Pedido?> GetDetailsByIdAsync(int id);

        Task UpdateAsync(Pedido pedido);
    }
}