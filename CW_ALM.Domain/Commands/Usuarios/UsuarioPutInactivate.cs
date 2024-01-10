using MediatR;

namespace CW_ALM.Domain.Commands.Usuarios
{
    public class UsuarioPutInactivate : IRequest<CommandResult>
    {
        public Guid UID { get; set; }
    }
}
