using FluentValidation;
using StorBookWebApp.DTOs.BookDTOs;
using StorBookWebApp.Shared.ValidatorSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.Shared.Validators.Book
{
    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            RuleFor(book => book.Title).Text();
            RuleFor(book => book.Description).Text();
            RuleFor(book => book.Isbn).Isbn();
            RuleFor(book => book.Publisher).Text();
        }
    }
}
