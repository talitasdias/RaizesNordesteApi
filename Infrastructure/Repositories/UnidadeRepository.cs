using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Interfaces;
using RaizesNordeste.API.Infrastructure.Persistence;

namespace RaizesNordeste.API.Infrastructure.Repositories
{
    public class UnidadeRepository : IUnidadeRepository
    {
        private readonly AppDbContext _dbContext;

        public UnidadeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unidade> Create(Unidade unidade)
        {
            _dbContext.Unidades.Add(unidade);
            await _dbContext.SaveChangesAsync();

            return unidade;
        }

        public async Task<(IEnumerable<Unidade>, int)> GetAllAsync(int pagina, int tamanhoPagina)
        {
            var query = _dbContext.Unidades.AsNoTracking();

            var total = await query.CountAsync();

            var unidades = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return (unidades, total);
        }

        public async Task<Unidade?> GetById(int id)
        {
            return await _dbContext.Unidades.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Unidades.AnyAsync(u => u.Id == id);
        }
    }
}