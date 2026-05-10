using RaizesNordeste.API.Domain.Entities;

namespace RaizesNordeste.API.Domain.Interfaces
{
    public interface IUnidadeRepository
    {
        Task<IEnumerable<Unidade>> GetAllAsync();
        Task<Unidade?> GetById(int id);
        Task<Unidade> Create(Unidade unidade);
    }
}