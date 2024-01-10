using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Usuarios;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Usuarios
{
    public class UsuarioGetAllHandler : IRequestHandler<UsuarioGetAll, CommandResult>
    {
        private readonly IUsuarioRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public UsuarioGetAllHandler(IUsuarioRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(UsuarioGetAll request, CancellationToken cancellationToken)
        {
            var usuarios = await _repository.GetAllAsync();
            return new CommandResult(true, _localizer["Resx_Domain_ListaDeUsuarios"].Value.ToString(), usuarios);
        }
    }
}
