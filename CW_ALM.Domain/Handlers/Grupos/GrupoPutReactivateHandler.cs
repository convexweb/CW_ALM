using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Grupos;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Grupos
{
    public class GrupoPutReactivateHandler : IRequestHandler<GrupoPutReactivate, CommandResult>
    {
        private readonly IGrupoRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public GrupoPutReactivateHandler(IGrupoRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(GrupoPutReactivate request, CancellationToken cancellationToken)
        {
            var grupo = _repository.GetByUID(request.UID);
            grupo.Reactivate();
            await _repository.UpdateAsync(grupo);
            return new CommandResult(true, _localizer["Resx_Domain_GrupoReativado"].Value.ToString(), grupo);
        }
    }
}
