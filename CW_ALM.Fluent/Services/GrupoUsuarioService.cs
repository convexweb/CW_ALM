using CW_ALM.Fluent.Services.Interfaces;
using CW_ALM.Fluent.ViewModels;

namespace CW_ALM.Fluent.Services
{
    public class GrupoUsuarioService : IGrupoUsuarioService
    {
        private IHttpService _httpService;
        private string _endPoint;

        public GrupoUsuarioService(IHttpService httpService)
        {
            _httpService = httpService;
            _endPoint = "gruposusuarios";
        }

        public async Task<CommandResultVM> GetByGrupoUIDAsync(Guid grupoUID)
        {
            var response = await _httpService.GetAsync($"/{_endPoint}/getbygrupouidasync/{grupoUID.ToString()}");
            return response;
        }

        public async Task<CommandResultVM> GetByUsuarioUIDAsync(Guid usuarioUID)
        {
            var response = await _httpService.GetAsync($"/{_endPoint}/getbyusuariouidasync/{usuarioUID.ToString()}");
            return response;
        }
    }
}
