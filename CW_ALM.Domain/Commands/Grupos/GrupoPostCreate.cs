using MediatR;

namespace CW_ALM.Domain.Commands.Grupos
{
    public class GrupoPostCreate : IRequest<CommandResult>
    {
        public string? Nome { get; set; }
    }
}
