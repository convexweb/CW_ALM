using CW_ALM.Fluent.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;

namespace CW_ALM.Fluent.Services.Interfaces
{
    public interface IAuthStateProvider : IScopedService
    {
        UsuarioVM CurrentUser { get; set; }
        Task Initialize();
        void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task);
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task<CommandResultVM> LoginAsync(string email, string password);
        void Logout();
    }
}
