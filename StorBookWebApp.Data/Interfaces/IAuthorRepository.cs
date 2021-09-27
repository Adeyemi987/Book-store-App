using StorBookWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.Data.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> Get(string id);
    }
}
