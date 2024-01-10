using CW_ALM.Client.ViewModels;
using CW_ALM.Client.Services.Interfaces;

namespace CW_ALM.Client.Services
{
    public class UserService : IUserService
    {
        private IHttpService _httpService;

        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _httpService.GetAsync<IEnumerable<User>>("/users");
        }
    }
}
