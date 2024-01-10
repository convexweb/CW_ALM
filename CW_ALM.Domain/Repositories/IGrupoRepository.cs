using CW_ALM.Domain.Core;
using CW_ALM.Domain.Entities;

namespace CW_ALM.Domain.Repositories
{
    public interface IGrupoRepository : IScopedService
    {
        Task CreateAsync(Grupo model);
        Task<List<Grupo>> GetAllAsync();
        Task<List<Grupo>> GetAllByStatusAsync(bool status);
        Grupo GetByUID(Guid uID);
        Grupo GetByUIDComLstUsuarios(Guid uID);
        Task<Grupo> GetByUIDAsync(Guid uID);
        Task UpdateAsync(Grupo model);
    }
}
