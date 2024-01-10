using MediatR;

namespace CW_ALM.Domain.Commands.Usuarios
{
    public class UsuarioGetByUID : IRequest<CommandResult>
    {
        public Guid UID { get; set; }
    }
}
