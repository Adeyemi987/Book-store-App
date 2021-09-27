using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.Shared.ValidatorSettings
{
    public static class BookValidatorExtension
    {
        public static IRuleBuilder<T, string> Text<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotNull().WithMessage("Book Title is required")
                .NotEmpty()
                .MinimumLength(2).WithMessage("Book Title must contain at least 2 characters");

            return options;
        }

        public static IRuleBuilder<T, string> Number<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotNull().WithMessage("Cannot be null")
                .NotEmpty().WithMessage("This is a required field")
                .Matches("[0-9]").WithMessage("Can only contain numbers");

            return options;
        }
        public static IRuleBuilder<T, string> Isbn<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotNull().WithMessage("Cannot be null")
                .Length(13).WithMessage("Please insert a 13 digits Isbn")
                .NotEmpty().WithMessage("This is a required field")
                .Matches("[0-9]").WithMessage("Can only contain numbers");

            return options;
        }
    }
}
