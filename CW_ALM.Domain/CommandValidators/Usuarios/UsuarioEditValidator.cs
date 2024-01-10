using CW_ALM.Domain.Commands.Usuarios;
using FluentValidation;

namespace CW_ALM.Domain.CommandValidators.Usuarios
{
    public class UsuarioEditValidator : AbstractValidator<UsuarioPutEdit>
    {
        public UsuarioEditValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty()
                .WithMessage("Resx_Domain_NomeObrigatorio");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("Resx_Domain_EmailObrigatorio")
                .EmailAddress().WithMessage("Resx_Domain_EmailInvalido");
            
            RuleFor(a => a.LstGrupos)
                .NotEmpty().WithMessage("Resx_Domain_EscolhaUmGrupo");
        }
    }
}
