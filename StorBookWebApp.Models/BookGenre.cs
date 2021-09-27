using System.ComponentModel.DataAnnotations;

namespace StorBookWebApp.Models
{
    public class BookGenre
    {

        [StringLength(125)]
        public string BookId { get; set; }
        public Book Book { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}