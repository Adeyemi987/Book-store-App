using FluentValidation;
using StorBookWebApp.DTOs.AuthorDTOs;
using StorBookWebApp.Shared.ValidatorSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.Shared.Validators.Author
{
    public class AddAuthorDtoValidator : AbstractValidator<AddAuthorDto>
    {
        public AddAuthorDtoValidator()
        {
            RuleFor(author => author.Name).HumanName();
        }
    }
}
