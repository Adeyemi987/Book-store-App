using FluentValidation;
using StorBookWebApp.DTOs.User;

namespace StorBookWebApp.Shared.Validators.User
{
    public class CreateUserRoleDTOValidator : AbstractValidator<CreateUserRoleDto>
    {
        public CreateUserRoleDTOValidator()
        {
            RuleFor(role => role.RoleName).NotNull().WithMessage("Name cannot be null")
                .NotEmpty().WithMessage("Role Name is required")
                .Matches("[A-Za-z]").WithMessage("Name can only contain alphabeths")
                .MinimumLength(2).WithMessage("Name is limited to a minimum of 2 characters")
                .MaximumLength(25).WithMessage("Name is limited to a maximum of 25 characters");
        }
    }
}
