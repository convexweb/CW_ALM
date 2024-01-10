using CW_ALM.Domain.Core;
using CW_ALM.Domain.Entities;

namespace CW_ALM.Domain.Repositories
{
    public interface IGrupoUsuarioRepository : IScopedService
    {
        Task CreateAsync(GrupoUsuario model);
        Task DeleteAsync(GrupoUsuario model);
        List<GrupoUsuario> GetByUsuarioUID(Guid usuarioUID);
        List<GrupoUsuario> GetByGrupoUID(Guid grupoUID);
    }
}
