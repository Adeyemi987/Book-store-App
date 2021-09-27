using StorBookWebApp.Data.Implementation;
using StorBookWebApp.Models;
using System;
using System.Threading.Tasks;

namespace StorBookWebApp.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<AppUser> _users;
        private IGenericRepository<Book> _books;
        private IGenericRepository<Genre> _genre;
        private IGenericRepository<Author> _author;
        private IGenericRepository<Category> _categories;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a new instance of GenericRepositoy<ApiUser> if _users is null
        /// </summary>
        public IGenericRepository<AppUser> AppUsers => _users ??= new GenericRepository<AppUser>(_context);
        public IGenericRepository<Book> Books => _books ??= new GenericRepository<Book>(_context);
        public IGenericRepository<Genre> Genres => _genre ??= new GenericRepository<Genre>(_context);
        public IGenericRepository<Author> Authors => _author ??= new GenericRepository<Author>(_context);
        public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
