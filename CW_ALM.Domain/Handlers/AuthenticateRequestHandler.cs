using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Authentication;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers
{
    public class AuthenticateRequestHandler : IRequestHandler<AuthenticateRequest, CommandResult>
    {
        private readonly IAuthenticationRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public AuthenticateRequestHandler(IAuthenticationRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            var response = await _repository.AuthenticateAsync(request);

            var resposta = new CommandResult(
                success: false,
                message: null,
                data: null);

            if (response is not null)
            {
                var jwtToken = _repository.GenerateJwtToken(response, request.SecretPhrase);

                AuthenticateResponse authResponse = new(
                    usuario: response,
                    token: jwtToken);

                resposta = new CommandResult(
                    success: true,
                    message: _localizer["Resx_Domain_UsuarioEncontrado"].Value.ToString(),
                    data: authResponse);
            } else
            {
                resposta = new CommandResult(
                    success: false,
                    message: _localizer["Resx_Domain_UsuarioNaoEncontrado"].Value.ToString(),
                    data: string.Empty);
                resposta.AddError("GenericError", _localizer["Resx_Domain_UsuarioNaoEncontrado"].Value.ToString());
            }
            
            return resposta;
        }
    }
}
