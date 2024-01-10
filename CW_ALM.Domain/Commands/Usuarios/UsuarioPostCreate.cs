using MediatR;

namespace CW_ALM.Domain.Commands.Usuarios
{
    public class UsuarioPostCreate : IRequest<CommandResult>
    {
        public string? UsuarioAD { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }

        public List<Guid>? LstGrupos { get; set; }
    }
}
