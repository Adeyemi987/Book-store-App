using StorBookWebApp.DTOs.BookDTOs;
using StorBookWebApp.Models;
using System.Linq;

namespace StorBookWebApp.Data.Interfaces
{
    public interface IBookRepository
    {
        IQueryable<BookListDto> MapBookToDto(IQueryable<Book> books);
        IQueryable<Book> GetAll();
        IQueryable<BookListDto> GetBooksByGenre(string genre);
    }
}
