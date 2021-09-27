using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StorBookWebApp.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Isbn { get; set; }

        [StringLength(125)]
        public string Title { get; set; }
        public string Description { get; set; }

        [StringLength(125)]
        public string Publisher { get; set; }

        [StringLength(250)]
        public string CoverUrl { get; set; }
        public string DownloadUrl { get; set; }
        public int PageCount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime Created { get; set; } = DateTime.Today;
        public DateTime Updated { get; set; } = DateTime.Today;
        public ICollection<BookGenre> BookGenres { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
