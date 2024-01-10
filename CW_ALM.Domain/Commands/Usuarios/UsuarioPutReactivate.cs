using MediatR;

namespace CW_ALM.Domain.Commands.Usuarios
{
    public class UsuarioPutReactivate : IRequest<CommandResult>
    {
        public Guid UID { get; set; }

        public UsuarioPutReactivate(Guid uID)
        {
            UID = uID;
        }
    }
}
