using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Enums;
using RaizesNordeste.API.Domain.Interfaces;
using RaizesNordeste.API.Infrastructure.Persistence;

namespace RaizesNordeste.API.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _dbContext;

        public PedidoRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Pedido> CreateAsync(Pedido pedido)
        {
            _dbContext.Pedidos.Add(pedido);
            await _dbContext.SaveChangesAsync();

            return pedido;
        }

        public async Task<(List<Pedido>, int)> GetByUsuarioIdAsync(
            int usuarioId,
            int pagina,
            int tamanhoPagina,
            CanalPedido? canalPedido,
            StatusPedido? statusPedido)
        {
            var query = _dbContext.Pedidos
                .Include(x => x.Itens)
                .ThenInclude(x => x.Produto)
                .Where(x => x.UsuarioId == usuarioId)
                .AsQueryable();

            if (canalPedido.HasValue)
                query = query.Where(x => x.CanalPedido == canalPedido.Value);

            if (statusPedido.HasValue)
                query = query.Where(x => x.StatusPedido == statusPedido.Value);

            var total = await query.CountAsync();

            var pedidos = await query
                .OrderByDescending(x => x.DataCriacao)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return (pedidos, total);
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _dbContext.Pedidos
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pedido?> GetDetailsByIdAsync(int id)
        {
            return await _dbContext.Pedidos
                .Include(x => x.Usuario)
                .Include(x => x.Itens)
                .ThenInclude(x => x.Produto)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            _dbContext.Pedidos.Update(pedido);

            await _dbContext.SaveChangesAsync();
        }
    }
}