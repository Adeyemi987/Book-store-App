using StorBookWebApp.Data;
using StorBookWebApp.DTOs.AuthorDTOs;
using StorBookWebApp.DTOs.BookDTOs;
using StorBookWebApp.DTOs.GenreDTOs;
using StorBookWebApp.DTOs.ImageDTOs;
using StorBookWebApp.Shared.ErrorResponses;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace StorBookWebApp.Core.API.Interfaces
{
    public interface IBookService
    {
        Task<BookListDto> AddBook(AddBookDto addBookDto, List<AddAuthorDto> authorsDto, List<AddGenreDto> genreDtos);
        BookListDto GetBookByIsbn(string isbn);
        BookListDto GetBookById(string id);
        IPagedList<BookListDto> GetBooks(RequestParams requestParams);
        Task<ApiResponse<IPagedList<BookListDto>>> GetBooksByGenre(string genre, RequestParams requestParams);
        Task<IList<BookDto>> SearchBooks(string term, RequestParams param);
        Task<bool> UpdateBook(string id, UpdateBookDto bookDto);
        Task<bool> UploadCoverUrlAsync(string bookId, AddImageDto imageDto);
        Task<bool> DeleteBook(string id);
    }
}
