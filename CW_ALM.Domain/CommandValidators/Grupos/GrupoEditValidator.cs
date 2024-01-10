using CW_ALM.Domain.Commands.Grupos;
using FluentValidation;

namespace CW_ALM.Domain.CommandValidators.Grupos
{
    public class GrupoEditValidator : AbstractValidator<GrupoPostCreate>
    {
        public GrupoEditValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty()
                .WithMessage("Resx_Domain_NomeObrigatorio");
        }
    }
}