using MediatR;

namespace CW_ALM.Domain.Commands.Grupos
{
    public class GrupoPutEdit : IRequest<CommandResult>
    {
        public Guid? UID { get; set; }
        public string? Nome { get; set; }
    }
}