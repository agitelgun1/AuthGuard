using AuthGuard.API.Models;
using FluentValidation;

namespace AuthGuard.API.Validators
{
    public class LoginValidator : AbstractValidator<TokenRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName cannot be empty!");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty!");
        }
    }
}