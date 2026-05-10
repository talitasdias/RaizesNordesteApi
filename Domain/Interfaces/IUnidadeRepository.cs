using RaizesNordeste.API.Domain.Entities;

namespace RaizesNordeste.API.Domain.Interfaces
{
    public interface IUnidadeRepository
    {
        Task<(IEnumerable<Unidade>, int)> GetAllAsync(int pagina, int tamanhoPagina);
        Task<Unidade?> GetById(int id);
        Task<Unidade> Create(Unidade unidade);
        Task<bool> ExistsAsync(int id);
    }
}