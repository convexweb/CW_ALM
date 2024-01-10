using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Grupos;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Grupos
{
    public class GrupoPutInactivateHandler : IRequestHandler<GrupoPutInactivate, CommandResult>
    {
        private readonly IGrupoRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public GrupoPutInactivateHandler(IGrupoRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(GrupoPutInactivate request, CancellationToken cancellationToken)
        {
            var usuario = _repository.GetByUID(request.UID);
            usuario.Inactivate();
            await _repository.UpdateAsync(usuario);
            return new CommandResult(true, _localizer["Resx_Domain_GrupoDesativado"].Value.ToString(), usuario);
        }
    }
}
