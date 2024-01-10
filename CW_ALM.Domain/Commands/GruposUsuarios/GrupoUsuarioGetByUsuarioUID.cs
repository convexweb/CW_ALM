using MediatR;

namespace CW_ALM.Domain.Commands.GruposUsuarios
{
    public class GrupoUsuarioGetByUsuarioUID : IRequest<CommandResult>
    {
        public Guid UsuarioUID { get; set; }
    }
}
