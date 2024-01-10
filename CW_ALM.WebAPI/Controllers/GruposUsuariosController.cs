using CW_ALM.Domain.Commands.Grupos;
using CW_ALM.Domain.Commands.GruposUsuarios;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using CW_ALM.WebAPI.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace CW_ALM.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GruposUsuariosController : ControllerBase
    {
        private IGrupoRepository _userService;
        private readonly IStringLocalizer<SharedResource_API> _localizer;
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;

        public GruposUsuariosController(
            IGrupoRepository userService,
            IStringLocalizer<SharedResource_API> localizer,
            IMediator mediator,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _localizer = localizer;
            _mediator = mediator;
            _appSettings = appSettings.Value;
        }


        [Authorize]
        [HttpGet("GetByUsuarioUIDAsync/{usuarioUID:guid}")]
        public async Task<IActionResult> GetByUsuarioUIDAsync(Guid usuarioUID)
        {
            var request = new GrupoUsuarioGetByUsuarioUID()
            {
                UsuarioUID = usuarioUID
            };
            var response = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetByGrupoUIDAsync/{grupoUID:guid}")]
        public async Task<IActionResult> GetByGrupoUIDAsync(Guid grupoUID)
        {
            var request = new GrupoUsuarioGetByGrupoUID()
            {
                GrupoUID = grupoUID
            };
            var response = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
