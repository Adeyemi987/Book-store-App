using StorBookWebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorBookWebApp.DTOs.AuthorDTOs
{
    public class AuthorDto
    {
        public string Id { get; set; }

        [StringLength(125)]
        public string Name { get; set; }
        public ICollection<Book> Books {  get; set; }
    }
}
