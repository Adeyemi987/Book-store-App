using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StorBookWebApp.Core.API.Implementations;
using StorBookWebApp.Core.API.Interfaces;
using StorBookWebApp.Data.Implementation;
using StorBookWebApp.Data.Implementations;
using StorBookWebApp.Data.Interfaces;
using StorBookWebApp.DTOs.API;
using StorBookWebApp.DTOs.AuthorDTOs;
using StorBookWebApp.DTOs.BookDTOs;
using StorBookWebApp.DTOs.User;
using StorBookWebApp.Shared.Validators.Author;
using StorBookWebApp.Shared.Validators.Book;
using StorBookWebApp.Shared.Validators.User;

namespace StorBookWebApp.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Service Injection
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IBookRepository, BookRepository>();

            // Repository Injection
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Fluent Validators
            //AppUser
            services.AddTransient<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddTransient<IValidator<LoginUserDto>, LoginUserDtoValidator>();
            services.AddTransient<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
            services.AddTransient<IValidator<CreateUserRoleDto>, CreateUserRoleDTOValidator>();
            //Book
            services.AddTransient<IValidator<AddBookDto>, AddBookDtoValidator>();
            services.AddTransient<IValidator<UpdateBookDto>, UpdateBookDtoValidator>();
            //Author
            services.AddTransient<IValidator<AddAuthorDto>, AddAuthorDtoValidator>();


        }
    }
}
