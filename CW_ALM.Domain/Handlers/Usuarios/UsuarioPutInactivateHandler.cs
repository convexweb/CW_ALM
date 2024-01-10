using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Usuarios;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Usuarios
{
    public class UsuarioPutInactivateHandler : IRequestHandler<UsuarioPutInactivate, CommandResult>
    {
        private readonly IUsuarioRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public UsuarioPutInactivateHandler(IUsuarioRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(UsuarioPutInactivate request, CancellationToken cancellationToken)
        {
            var usuario = _repository.GetByUID(request.UID);
            usuario.Inactivate();
            await _repository.UpdateAsync(usuario);
            return new CommandResult(true, _localizer["Resx_Domain_UsuarioDesativado"].Value.ToString(), usuario);
        }
    }
}
