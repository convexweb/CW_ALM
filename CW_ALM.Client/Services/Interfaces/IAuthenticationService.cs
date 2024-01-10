using CW_ALM.Client.ViewModels;

namespace CW_ALM.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {
        User User { get; }
        Task Initialize();
        Task Login(string email, string password);
        Task Logout();
    }
}
