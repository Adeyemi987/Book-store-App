using System;

namespace StorBookWebApp.DTOs.BookDTOs
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Authors { get; set; }
        public string Publisher { get; set; }
        public string CoverUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
