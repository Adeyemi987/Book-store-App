using System;
using System.ComponentModel.DataAnnotations;

namespace StorBookWebApp.Models
{
    public class Review
    {

        [StringLength(125)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int NumStars { get; set; }
        public string Comment { get; set; }

        [StringLength(125)]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        [StringLength(125)]
        public string BookId { get; set; }
        public Book Book { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
