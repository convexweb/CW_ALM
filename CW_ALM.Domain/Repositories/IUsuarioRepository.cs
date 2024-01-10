using CW_ALM.Domain.Core;
using CW_ALM.Domain.Entities;

namespace CW_ALM.Domain.Repositories
{
    public interface IUsuarioRepository : IScopedService
    {
        Task CreateAsync(Usuario model);
        Task<List<Usuario>> GetAllAsync();
        Task<List<Usuario>> GetAllByStatusAsync(bool status);
        Usuario GetByUID(Guid uID);
        Usuario GetByUIDComLstGrupos(Guid uID);
        Task<Usuario> GetByUIDAsync(Guid uID);
        Task<Usuario> GetUsuarioByEmailSenhaAsync(string email, string senha);
        Task UpdateAsync(Usuario model);
    }
}
