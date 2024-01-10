using MediatR;

namespace CW_ALM.Domain.Commands.GruposUsuarios
{
    public class GrupoUsuarioGetByGrupoUID : IRequest<CommandResult>
    {
        public Guid GrupoUID { get; set; }
    }
}
