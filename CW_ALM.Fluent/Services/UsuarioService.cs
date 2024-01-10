using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;

namespace CW_ALM.Fluent.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IHttpService _httpService;
        private string _endPoint;

        public UsuarioService(IHttpService httpService)
        {
            _httpService = httpService;
            _endPoint = "usuarios";
        }

        public async Task<CommandResultVM> CreateAsync(UsuarioVM model)
        {
            var response = await _httpService.PostAsync($"/{_endPoint}", model);
            return response;
        }

        public async Task<CommandResultVM> EditAsync(UsuarioVM model)
        {
            var response = await _httpService.PutAsync($"/{_endPoint}/updateasync", model);
            return response;
        }

        public async Task<CommandResultVM> GetAllAsync()
        {
            var response = await _httpService.GetAsync($"/{_endPoint}");
            return response;
        }

        public async Task<CommandResultVM> InactivateAsync(UsuarioVM model)
        {
            var response = await _httpService.PutAsync($"/{_endPoint}/inactivateasync", model);
            return response;
        }

        public async Task<CommandResultVM> ReactivateAsync(UsuarioVM model)
        {
            var response = await _httpService.PutAsync($"/{_endPoint}/reactivateasync", model);
            return response;
        }
    }
}
