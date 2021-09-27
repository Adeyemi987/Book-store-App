using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorBookWebApp.Data.Interfaces;
using StorBookWebApp.DTOs.AuthorDTOs;
using StorBookWebApp.DTOs.BookDTOs;
using StorBookWebApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StorBookWebApp.Data.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<BookListDto> MapBookToDto(IQueryable<Book> books)
        {
            return books.Select(book => new BookListDto
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                PublishedOn = book.Publisher,
                Description = book.Description,
                Thumbnail = book.CoverUrl,
                AuthorsOrdered = string.Join(", ", book.BookAuthors
                                    .OrderBy(ba => ba.Author.Name)
                                    .Select(ba => ba.Author.Name)),
                ReviewsCount = book.Reviews.Count,
                ReviesAverageCount = book.Reviews.Select(review =>
                                (double?)review.NumStars).Average(),
                Created = book.Created,
                Updated = book.Updated
            });
        }

        public IQueryable<Book> GetAll()
        {
            return _context.Books.Include(x => x.Category).AsNoTracking();
        }

        public Review GetBlankReview(string id)
        {
            string BookTitle = _context.Books
                .Where(book => book.Id == id)
                .Select(book => book.Title)
                .Single();

            return new Review
            {
                BookId = id
            };
        }

        public async Task<Book> AddReviewToBook(Review review)
        {
            var book = _context.Books
                .Include(r => r.Reviews)
                .Single(k => k.Id == review.BookId);

            book.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return book;
        }

        public IQueryable<BookListDto> GetBooksByGenre(string genre)
        {
            var books = GetAll()
                .Include(bg => bg.BookGenres)
                .Where(x => x.BookGenres.Any(y => y.Genre.Name == genre));
            var result = MapBookToDto(books);

            return result;
                
        }


    }
}
