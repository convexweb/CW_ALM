using CW_ALM.Domain.Commands;
using CW_ALM.Domain.Commands.Usuarios;
using CW_ALM.Domain.Entities;
using CW_ALM.Domain.Repositories;
using CW_ALM.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CW_ALM.Domain.Handlers.Usuarios
{
    public class UsuarioPostCreateHandler : IRequestHandler<UsuarioPostCreate, CommandResult>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        IStringLocalizer<SharedResource_Domain> _localizer;

        public UsuarioPostCreateHandler(IUsuarioRepository usuarioRepo, IStringLocalizer<SharedResource_Domain> localizer)
        {
            _usuarioRepo = usuarioRepo;
            _localizer = localizer;
        }

        public async Task<CommandResult> Handle(UsuarioPostCreate request, CancellationToken cancellationToken)
        {
            var retorno = new CommandResult(success: false, message: string.Empty, data: null);

            try
            {
                var usuario = new Usuario();
                usuario.Create(usuarioAD: request.UsuarioAD, nome: request.Nome, email: request.Email, senha: request.Senha);
                foreach (Guid grupoUID in request.LstGrupos)
                {
                    usuario.LstGruposUsuarios.Add(new GrupoUsuario(grupoUID: grupoUID, usuarioUID: usuario.UID));
                }
                await _usuarioRepo.CreateAsync(usuario);

                retorno.Success = true;
                retorno.Message = _localizer["Resx_Domain_UsuarioCriado"].Value.ToString();
                retorno.Data = usuario;
            }
            catch
            {
                retorno.Success = false;
                retorno.Message = _localizer["Resx_Domain_UsuarioNaoFoiCriado"].Value.ToString();
                retorno.Data = null;
                retorno.AddError("GenericError", _localizer["Resx_Domain_ErroAoSalvarRegistro"].Value.ToString());
            }
            return retorno;
        }
    }
}
