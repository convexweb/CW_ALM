using CW_ALM.Domain.Commands.Authentication;
using FluentValidation;

namespace CW_ALM.Domain.CommandValidators.Authentication
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("Resx_Domain_EmailObrigatorio");

            RuleFor(a => a.Password)
                .NotEmpty()
                .WithMessage("Resx_Domain_SenhaObrigatorio");
            
        }
    }
}
