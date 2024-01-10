using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Usuarios;
using CW_ALM.Domain.Entities;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Usuarios
{
    public class UsuarioPutEditHandler : IRequestHandler<UsuarioPutEdit, CommandResult>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepo;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public UsuarioPutEditHandler(IUsuarioRepository usuarioRepo, IGrupoUsuarioRepository grupoUsuarioRepo, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _usuarioRepo = usuarioRepo;
            _grupoUsuarioRepo = grupoUsuarioRepo;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(UsuarioPutEdit request, CancellationToken cancellationToken)
        {
            if (!request.UID.HasValue || request.UID.Value.Equals(Guid.Empty)) 
                return new CommandResult(true, _localizer["Resx_Domain_UsuarioNaoFoiAlterado"].Value.ToString(), null);

            var retorno = new CommandResult(success: false, message: string.Empty, data: null);

            try
            {
                var usuario = _usuarioRepo.GetByUID(request.UID.Value);
                usuario.Update(nome: request.Nome, email: request.Email, senha: request.Senha);                
                foreach (var grupoUsuario in _grupoUsuarioRepo.GetByUsuarioUID(usuario.UID))
                {
                    await _grupoUsuarioRepo.DeleteAsync(grupoUsuario);
                }
                foreach (Guid grupoUID in request.LstGrupos)
                {
                    await _grupoUsuarioRepo.CreateAsync(new GrupoUsuario(grupoUID: grupoUID, usuarioUID: usuario.UID));
                }
                await _usuarioRepo.UpdateAsync(usuario);


                retorno.Success = true;
                retorno.Message = _localizer["Resx_Domain_UsuarioAlterado"].Value.ToString();
                retorno.Data = usuario;
            }
            catch
            {
                retorno.Success = false;
                retorno.Message = _localizer["Resx_Domain_UsuarioNaoFoiAlterado"].Value.ToString();
                retorno.Data = null;
                retorno.AddError("GenericError", _localizer["Resx_Domain_ErroAoSalvarRegistro"].Value.ToString());
            }
            return retorno;
        }
    }
}
