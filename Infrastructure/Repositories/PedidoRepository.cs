using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Domain.Entities;
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

        public async Task<List<Pedido>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _dbContext.Pedidos
                .Include(x => x.Itens)
                .ThenInclude(x => x.Produto)
                .Where(x => x.UsuarioId == usuarioId)
                .OrderByDescending(x => x.DataCriacao)
                .ToListAsync();
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