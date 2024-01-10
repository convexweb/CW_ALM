using CW_ALM.Domain.Commands.Grupos;
using FluentValidation;

namespace CW_ALM.Domain.CommandValidators.Grupos
{
    public class GrupoCreateValidator : AbstractValidator<GrupoPostCreate>
    {
        public GrupoCreateValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty()
                .WithMessage("Resx_Domain_NomeObrigatorio");
        }
    }
}