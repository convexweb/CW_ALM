using MediatR;

namespace CW_ALM.Domain.Commands.Usuarios
{
    public class UsuarioPutEdit : IRequest<CommandResult>
    {
        public Guid? UID { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }

        public List<Guid>? LstGrupos { get; set; }
    }
}
