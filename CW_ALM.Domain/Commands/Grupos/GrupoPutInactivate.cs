using MediatR;

namespace CW_ALM.Domain.Commands.Grupos
{
    public class GrupoPutInactivate : IRequest<CommandResult>
    {
        public Guid UID { get; set; }
    }
}
