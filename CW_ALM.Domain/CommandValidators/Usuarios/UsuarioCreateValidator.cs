using CW_ALM.Domain.Commands.Usuarios;
using FluentValidation;

namespace CW_ALM.Domain.CommandValidators.Usuarios
{
    public class UsuarioCreateValidator : AbstractValidator<UsuarioPostCreate>
    {
        public UsuarioCreateValidator()
        {
            RuleFor(a => a.UsuarioAD)
                .NotEmpty().WithMessage("Resx_Domain_UsuarioADObrigatorio");

            RuleFor(a => a.Nome)
                .NotEmpty()
                .WithMessage("Resx_Domain_NomeObrigatorio");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("Resx_Domain_EmailObrigatorio")
                .EmailAddress().WithMessage("Resx_Domain_EmailInvalido");

            RuleFor(a => a.Senha)
                .NotEmpty().WithMessage("Resx_Domain_SenhaObrigatorio");

            RuleFor(a => a.LstGrupos)
                .NotEmpty().WithMessage("Resx_Domain_EscolhaUmGrupo");
        }
    }
}
