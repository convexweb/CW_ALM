using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Grupos;
using CW_ALM.Domain.Entities;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Grupos
{
    public class GrupoPostCreateHandler : IRequestHandler<GrupoPostCreate, CommandResult>
    {
        private readonly IGrupoRepository _grupoRepo;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public GrupoPostCreateHandler(IGrupoRepository grupoRepo, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _grupoRepo = grupoRepo;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(GrupoPostCreate request, CancellationToken cancellationToken)
        {
            var retorno = new CommandResult(success: false, message: string.Empty, data: null);

            try
            {
                var usuario = new Grupo(nome: request.Nome);
                await _grupoRepo.CreateAsync(usuario);

                retorno.Success = true;
                retorno.Message = _localizer["Resx_Domain_GrupoCriado"].Value.ToString();
                retorno.Data = usuario;
            }
            catch
            {
                retorno.Success = false;
                retorno.Message = _localizer["Resx_Domain_GrupoNaoFoiCriado"].Value.ToString();
                retorno.Data = null;
                retorno.AddError("GenericError", _localizer["Resx_Domain_ErroAoSalvarRegistro"].Value.ToString());
            }
            return retorno;
        }
    }
}
