using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Grupos;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Grupos
{
    public class GrupoGetAllHandler : IRequestHandler<GrupoGetAll, CommandResult>
    {
        private readonly IGrupoRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public GrupoGetAllHandler(IGrupoRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(GrupoGetAll request, CancellationToken cancellationToken)
        {
            var grupos = await _repository.GetAllAsync();
            return new CommandResult(true, _localizer["Resx_Domain_ListaDeGrupos"].Value.ToString(), grupos);
        }
    }
}
