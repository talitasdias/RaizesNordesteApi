using RaizesNordeste.API.Domain.Entities;

namespace RaizesNordeste.API.Domain.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<Pagamento> CreateAsync(Pagamento pagamento);
    }
}