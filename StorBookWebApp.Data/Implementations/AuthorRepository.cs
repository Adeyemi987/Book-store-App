using Microsoft.EntityFrameworkCore;
using StorBookWebApp.DTOs.AuthorDTOs;
using StorBookWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.Data.Implementations
{
    public class AuthorRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Author> Get(string id)
        {
            return await _context.Authors.AsNoTracking().SingleOrDefaultAsync(author => author.Id == id);
        }

    }
}
