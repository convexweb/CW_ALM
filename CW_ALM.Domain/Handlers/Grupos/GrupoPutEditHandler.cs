using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Grupos;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Grupos
{
    public class GrupoPutEditHandler : IRequestHandler<GrupoPutEdit, CommandResult>
    {
        private readonly IGrupoRepository _grupoRepo;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public GrupoPutEditHandler(IGrupoRepository grupoRepo, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _grupoRepo = grupoRepo;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(GrupoPutEdit request, CancellationToken cancellationToken)
        {
            if (!request.UID.HasValue || request.UID.Value.Equals(Guid.Empty)) 
                return new CommandResult(true, _localizer["Resx_Domain_GrupoNaoFoiAlterado"].Value.ToString(), null);

            var retorno = new CommandResult(success: false, message: string.Empty, data: null);

            try
            {
                var usuario = _grupoRepo.GetByUID(request.UID.Value);
                usuario.Update(nome: request.Nome);
                await _grupoRepo.UpdateAsync(usuario);


                retorno.Success = true;
                retorno.Message = _localizer["Resx_Domain_GrupoAlterado"].Value.ToString();
                retorno.Data = usuario;
            }
            catch
            {
                retorno.Success = false;
                retorno.Message = _localizer["Resx_Domain_GrupoNaoFoiAlterado"].Value.ToString();
                retorno.Data = null;
                retorno.AddError("GenericError", _localizer["Resx_Domain_ErroAoSalvarRegistro"].Value.ToString());
            }
            return retorno;
        }
    }
}
