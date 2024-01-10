﻿using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.GruposUsuarios;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.GruposUsuarios
{
    public class GrupoUsuarioGetByUsuarioUIDHandler : IRequestHandler<GrupoUsuarioGetByUsuarioUID, CommandResult>
    {
        private readonly IGrupoUsuarioRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public GrupoUsuarioGetByUsuarioUIDHandler(IGrupoUsuarioRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(GrupoUsuarioGetByUsuarioUID request, CancellationToken cancellationToken)
        {
            var grupo = _repository.GetByUsuarioUID(request.UsuarioUID);
            return new CommandResult(true, _localizer["Resx_Domain_LstGrupoUsuarioPorUsuarioUID"].Value.ToString(), grupo);
        }
    }
}