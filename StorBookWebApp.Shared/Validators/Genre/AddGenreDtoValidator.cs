using FluentValidation;
using StorBookWebApp.DTOs.GenreDTOs;
using StorBookWebApp.Shared.ValidatorSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.Shared.Validators.Genre
{
    public class AddGenreDtoValidator : AbstractValidator<AddGenreDto>
    {
        public AddGenreDtoValidator()
        {
            RuleFor(genre => genre.Name).Text();
        }
    }
}
