using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.DTOs.ReviewDTOs
{
    public class AddReviewDto
    {
        [StringLength(125)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int NumStars { get; set; }
        public string Comment { get; set; }

        [StringLength(125)]
        public string UserId { get; set; }

        [StringLength(125)]
        public string BookId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
