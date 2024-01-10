using CW_ALM.Domain.Commands.Grupos;
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
    public class GruposController : ControllerBase
    {
        private IGrupoRepository _userService;
        private readonly IStringLocalizer<SharedResource_API> _localizer;
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;

        public GruposController(
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
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GrupoPostCreate request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response == null || response.Success.Equals(false))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var request = new GrupoGetAll();
            var response = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetAllByStatusAsync/{status:bool}")]
        public async Task<IActionResult> GetAllByStatusAsync(bool status)
        {
            var request = new GrupoGetAllByStatus()
            {
                Status = status
            };
            var response = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetByUIDAsync/{uID:guid}")]
        public async Task<IActionResult> GetByUIDAsync(Guid uID)
        {
            var request = new GrupoGetByUID()
            {
                UID = uID
            };
            var response = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(response);
        }
        [Authorize]
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] GrupoPutEdit request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(response);
        }
        [Authorize]
        [HttpPut("InactivateAsync")]
        public async Task<IActionResult> InactivateAsync([FromBody] GrupoPutInactivate request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(response);
        }
        [Authorize]
        [HttpPut("ReactivateAsync")]
        public async Task<IActionResult> ReactivateAsync([FromBody] GrupoPutReactivate request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
