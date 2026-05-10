using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Interfaces;
using RaizesNordeste.API.Infrastructure.Persistence;

namespace RaizesNordeste.API.Infrastructure.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly AppDbContext _context;

    public PagamentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pagamento> CreateAsync(Pagamento pagamento)
    {
        _context.Pagamentos.Add(pagamento);

        await _context.SaveChangesAsync();

        return pagamento;
    }
    }
}