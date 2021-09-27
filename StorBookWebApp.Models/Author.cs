using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorBookWebApp.Models
{
    public class Author
    {
        public string Id { get; set; }

        [StringLength(125)]
        public string Name { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}