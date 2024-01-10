﻿using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Usuarios;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Usuarios
{
    public class UsuarioPutReactivateHandler : IRequestHandler<UsuarioPutReactivate, CommandResult>
    {
        private readonly IUsuarioRepository _repository;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public UsuarioPutReactivateHandler(IUsuarioRepository repository, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(UsuarioPutReactivate request, CancellationToken cancellationToken)
        {
            var usuario = _repository.GetByUID(request.UID);
            usuario.Reactivate();
            await _repository.UpdateAsync(usuario);
            return new CommandResult(true, _localizer["Resx_Domain_UsuarioReativado"].Value.ToString(), usuario);
        }
    }
}
