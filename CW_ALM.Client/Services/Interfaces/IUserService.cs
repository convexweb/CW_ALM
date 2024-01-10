using CW_ALM.Client.ViewModels;

namespace CW_ALM.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
    }
}
