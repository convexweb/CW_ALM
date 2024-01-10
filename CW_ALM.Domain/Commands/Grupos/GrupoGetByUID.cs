using MediatR;

namespace CW_ALM.Domain.Commands.Grupos
{
    public class GrupoGetByUID : IRequest<CommandResult>
    {
        public Guid UID { get; set; }
    }
}
