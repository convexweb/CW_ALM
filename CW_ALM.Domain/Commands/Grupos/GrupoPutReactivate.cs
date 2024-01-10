using MediatR;

namespace CW_ALM.Domain.Commands.Grupos
{
    public class GrupoPutReactivate : IRequest<CommandResult>
    {
        public Guid UID { get; set; }
    }
}
