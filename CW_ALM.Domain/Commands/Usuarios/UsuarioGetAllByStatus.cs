using MediatR;

namespace CW_ALM.Domain.Commands.Usuarios
{
    public class UsuarioGetAllByStatus : IRequest<CommandResult>
    {
        public bool Status { get; set; } = true;
    }
}
