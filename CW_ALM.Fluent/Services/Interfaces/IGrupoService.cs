using CW_ALM.Fluent.ViewModels;

namespace CW_ALM.Fluent.Services.Interfaces
{
    public interface IGrupoService : IScopedService
    {
        Task<CommandResultVM> CreateAsync(GrupoVM model);
        Task<CommandResultVM> EditAsync(GrupoVM model);
        Task<CommandResultVM> GetAllAsync();
        Task<CommandResultVM> InactivateAsync(GrupoVM model);
        Task<CommandResultVM> ReactivateAsync(GrupoVM model);
    }
}
