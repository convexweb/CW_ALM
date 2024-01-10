using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;

namespace CW_ALM.Fluent.Services
{
    public class GrupoService : IGrupoService
    {
        private IHttpService _httpService;
        private string _endPoint;

        public GrupoService(IHttpService httpService)
        {
            _httpService = httpService;
            _endPoint = "grupos";
        }

        public async Task<CommandResultVM> CreateAsync(GrupoVM model)
        {
            var response = await _httpService.PostAsync($"/{_endPoint}", model);
            return response;
        }

        public async Task<CommandResultVM> EditAsync(GrupoVM model)
        {
            var response = await _httpService.PutAsync($"/{_endPoint}/updateasync", model);
            return response;
        }

        public async Task<CommandResultVM> GetAllAsync()
        {
            var response = await _httpService.GetAsync($"/{_endPoint}");
            return response;
        }

        public async Task<CommandResultVM> InactivateAsync(GrupoVM model)
        {
            var response = await _httpService.PutAsync($"/{_endPoint}/inactivateasync", model);
            return response;
        }

        public async Task<CommandResultVM> ReactivateAsync(GrupoVM model)
        {
            var response = await _httpService.PutAsync($"/{_endPoint}/reactivateasync", model);
            return response;
        }
    }
}
