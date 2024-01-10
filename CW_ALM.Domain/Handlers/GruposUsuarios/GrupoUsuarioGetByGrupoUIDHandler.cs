using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.GruposUsuarios;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.GruposUsuarios
{
    public class GrupoUsuarioGetByGrupoUIDHandler : IRequestHandler<GrupoUsuarioGetByGrupoUID, CommandResult>
    {
        private readonly IGrupoUsuarioRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public GrupoUsuarioGetByGrupoUIDHandler(IGrupoUsuarioRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(GrupoUsuarioGetByGrupoUID request, CancellationToken cancellationToken)
        {
            var grupo = _repository.GetByGrupoUID(request.GrupoUID);
            return new CommandResult(true, _localizer["Resx_Domain_LstGrupoUsuarioPorGrupoUID"].Value.ToString(), grupo);
        }
    }
}