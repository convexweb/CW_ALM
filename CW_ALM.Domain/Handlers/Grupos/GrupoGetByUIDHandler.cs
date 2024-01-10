using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Grupos;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Grupos
{
    public class GrupoGetByUIDHandler : IRequestHandler<GrupoGetByUID, CommandResult>
    {
        private readonly IGrupoRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public GrupoGetByUIDHandler(IGrupoRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(GrupoGetByUID request, CancellationToken cancellationToken)
        {
            var grupo = await _repository.GetByUIDAsync(request.UID);
            return new CommandResult(true, _localizer["Resx_Domain_Grupo"].Value.ToString(), grupo);
        }
    }
}