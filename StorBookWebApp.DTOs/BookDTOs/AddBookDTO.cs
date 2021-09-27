using StorBookWebApp.DTOs.AuthorDTOs;
using StorBookWebApp.DTOs.GenreDTOs;
using System;
using System.Collections.Generic;

namespace StorBookWebApp.DTOs.BookDTOs
{
    public class AddBookDto
    {
        public string Id { get; set; } = new Guid().ToString();
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<AuthorDto> Authors { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public string Publisher { get; set; }
        public string CoverUrl { get; set; }
        public string CategoryId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
