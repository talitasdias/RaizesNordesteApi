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

        public async Task<IEnumerable<Unidade>> GetAllAsync()
        {
            return await _dbContext.Unidades.AsNoTracking().ToListAsync();
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