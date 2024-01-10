using CW_ALM.Fluent.ViewModels;

namespace CW_ALM.Fluent.Services.Interfaces
{
    public interface IHttpService : IScopedService
    {
        Task<CommandResultVM> GetAsync(string uri);
        Task<CommandResultVM> PostAsync(string uri, object value);
        Task<CommandResultVM> PutAsync(string uri, object value);
    }
}
