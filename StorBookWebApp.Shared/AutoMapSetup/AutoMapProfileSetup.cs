using AutoMapper;
using StorBookWebApp.DTOs.API;
using StorBookWebApp.DTOs.BookDTOs;
using StorBookWebApp.DTOs.GenreDTOs;
using StorBookWebApp.Models;

namespace StorBookWebApp.Shared.AutoMapSetup
{
    public class AutoMapProfileSetup : Profile
    {
        public AutoMapProfileSetup()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, UpdateUserDto>().ReverseMap();
            CreateMap<AppUser, RegisterUserDto>().ReverseMap();
            CreateMap<AppUser, LoginUserDto>().ReverseMap();

            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, AddBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
            CreateMap<Book, BookListDto>().ReverseMap();

            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Genre, AddGenreDto>().ReverseMap();
        }
        
    }
}
