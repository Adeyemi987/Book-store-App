using FluentValidation;
using StorBookWebApp.DTOs.API;
using StorBookWebApp.Shared.ValidatorSettings;

namespace StorBookWebApp.Shared.Validators.User
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(user => user.FirstName).HumanName();

            RuleFor(user => user.LastName).HumanName();

            RuleFor(user => user.PhoneNumber).PhoneNumber();

            RuleFor(user => user.Email).EmailAddress();

            RuleFor(user => user.Password).Password();
        }
    }
}
