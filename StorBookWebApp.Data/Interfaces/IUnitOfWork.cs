using StorBookWebApp.Models;
using System;
using System.Threading.Tasks;

namespace StorBookWebApp.Data.Implementation
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Book> Books { get; }
        IGenericRepository<AppUser> AppUsers { get; }
        IGenericRepository<Genre> Genres { get; }
        IGenericRepository<Author> Authors { get; }
        IGenericRepository<Category> Categories { get; }
        Task Save();
    }
}
