using System.ComponentModel.DataAnnotations;

namespace StorBookWebApp.Models
{
    public class BookAuthor
    {

        [StringLength(125)]
        public string BookId { get; set; }
        public Book Book { get; set; }

        [StringLength(125)]
        public string AuthorId { get; set; }
        public Author Author { get; set; }
    }
}