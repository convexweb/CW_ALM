using CW_ALM.Fluent.ViewModels;

namespace CW_ALM.Fluent.Services.Interfaces
{
    public interface IGrupoUsuarioService : IScopedService
    {
        Task<CommandResultVM> GetByUsuarioUIDAsync(Guid usuarioUID);
        Task<CommandResultVM> GetByGrupoUIDAsync(Guid grupoUID);
    }
}
