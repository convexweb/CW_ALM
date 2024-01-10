using CW_ALM.Fluent.ViewModels;

namespace CW_ALM.Fluent.Services.Interfaces
{
    public interface IUserService : IScopedService
    {
        Task<CommandResultVM> GetAll();
    }
}
