using FluentValidation;
using StorBookWebApp.DTOs.API;
using StorBookWebApp.Shared.ValidatorSettings;

namespace StorBookWebApp.Shared.Validators.User
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(user => user.Email).EmailAddress();

            RuleFor(user => user.Password).Password();
        }
    }
}
