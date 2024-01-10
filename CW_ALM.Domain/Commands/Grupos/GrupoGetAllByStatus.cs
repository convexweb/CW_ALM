using MediatR;

namespace CW_ALM.Domain.Commands.Grupos
{
    public class GrupoGetAllByStatus : IRequest<CommandResult>
    {
        public bool Status { get; set; } = true;
    }
}
