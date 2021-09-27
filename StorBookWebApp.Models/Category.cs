using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorBookWebApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(125)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
        public bool IsActive { get; set; }
    }
}
