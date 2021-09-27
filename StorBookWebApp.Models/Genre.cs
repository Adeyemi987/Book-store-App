using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorBookWebApp.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [StringLength(125)]
        public string Name { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}
