using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Usuarios;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Usuarios
{
    public class UsuarioGetByUIDHandler : IRequestHandler<UsuarioGetByUID, CommandResult>
    {
        private readonly IUsuarioRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public UsuarioGetByUIDHandler(IUsuarioRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(UsuarioGetByUID request, CancellationToken cancellationToken)
        {
            var usuario = await _repository.GetByUIDAsync(request.UID);
            return new CommandResult(true, _localizer["Resx_Domain_Usuario"].Value.ToString(), usuario);
        }
    }
}