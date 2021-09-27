using FluentValidation;
using StorBookWebApp.DTOs.API;
using StorBookWebApp.Shared.ValidatorSettings;

namespace StorBookWebApp.Shared.Validators.User
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(user => user.FirstName).HumanName();

            RuleFor(user => user.LastName).HumanName();

            RuleFor(user => user.PhoneNumber).PhoneNumber();
        }
    }
}
