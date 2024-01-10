using CW_ALM.Domain.Commands.Authentication;
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
    public class AuthenticationController : Controller
    {
        private IUsuarioRepository _usuarioService;
        private readonly IStringLocalizer<SharedResource_API> _localizer;
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;

        public AuthenticationController(IUsuarioRepository usuarioService,
            IStringLocalizer<SharedResource_API> localizer,
            IMediator mediator,
            IOptions<AppSettings> appSettings)
        {
            _usuarioService = usuarioService;
            _localizer = localizer;
            _mediator = mediator;
            _appSettings = appSettings.Value;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            model.SecretPhrase = _appSettings.Secret;
            var response = await _mediator.Send(model).ConfigureAwait(false);
            
            if (response == null || response.Success.Equals(false))
                return BadRequest(response);

            return Ok(response);
        }
    }
}
