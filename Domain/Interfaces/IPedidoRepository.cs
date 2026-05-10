using RaizesNordeste.API.Domain.Entities;

namespace RaizesNordeste.API.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> CreateAsync(Pedido pedido);

        Task<List<Pedido>> GetByUsuarioIdAsync(int usuarioId);

        Task<Pedido?> GetByIdAsync(int id);

        Task<Pedido?> GetDetailsByIdAsync(int id);

        Task UpdateAsync(Pedido pedido);
    }
}