using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Interfaces;
using RaizesNordeste.API.Infrastructure.Persistence;

namespace RaizesNordeste.API.Infrastructure.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly AppDbContext _context;

        public EstoqueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estoque>> GetAllAsync()
        {
            return _context.Estoques
                .Include(x => x.Produto)
                .Include(x => x.Unidade)
                .ToList();
        }

        public async Task<Estoque?> GetByProdutoAndUnidadeAsync(int produtoId, int unidadeId)
        {
            return await _context.Estoques
                .FirstOrDefaultAsync(x =>
                    x.ProdutoId == produtoId &&
                    x.UnidadeId == unidadeId);
        }

        public async Task<IEnumerable<Estoque>> GetByIdUnidadeAsync(int unidadeId)
        {
            return await _context.Estoques
                    .Where(x => x.UnidadeId == unidadeId)
                    .Include(x => x.Produto)
                    .Include(x => x.Unidade)
                    .ToListAsync();
        }

        public async Task<Estoque?> GetByIdAsync(int id)
        {
            return await _context.Estoques
                .Include(x => x.Produto)
                .Include(x => x.Unidade)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Estoque> CreateAsync(Estoque estoque)
        {
            _context.Estoques.Add(estoque);

            await _context.SaveChangesAsync();

            return estoque;
        }

        public async Task UpdateAsync(Estoque estoque)
        {
            _context.Estoques.Update(estoque);

            await _context.SaveChangesAsync();
        }
    }
}