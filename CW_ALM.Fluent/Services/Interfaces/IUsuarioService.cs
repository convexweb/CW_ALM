using CW_ALM.Fluent.ViewModels;

namespace CW_ALM.Fluent.Services.Interfaces
{
    public interface IUsuarioService : IScopedService
    {
        Task<CommandResultVM> GetAllAsync();
        Task<CommandResultVM> CreateAsync(UsuarioVM model);
        Task<CommandResultVM> EditAsync(UsuarioVM model);
        Task<CommandResultVM> InactivateAsync(UsuarioVM model);
        Task<CommandResultVM> ReactivateAsync(UsuarioVM model);
    }
}
