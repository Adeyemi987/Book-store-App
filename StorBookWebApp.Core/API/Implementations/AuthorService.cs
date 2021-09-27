using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorBookWebApp.Data.Interfaces;
using StorBookWebApp.DTOs.AuthorDTOs;
using System.Linq;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Implementations
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepo, IMapper mapper,
            IBookRepository bookRepo)
        {
            _authorRepo = authorRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<AuthorDto> GetAuthorWithBooks(string id)
        {
            var books = await _bookRepo.GetAll()
                .Include(X => X.BookAuthors)
                .ThenInclude(x => x.AuthorId == id)
                .OrderBy(x => x.Title).ToListAsync();

            var author = _authorRepo.Get(id);
            
            var result = _mapper.Map<AuthorDto>(author);
            result.Books = books;

            return result;
        }
    }
}
