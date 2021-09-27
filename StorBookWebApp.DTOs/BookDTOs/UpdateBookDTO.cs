using Microsoft.AspNetCore.Http;

namespace StorBookWebApp.DTOs.BookDTOs
{
    public class UpdateBookDto
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public IFormFile Image { get; set; }
    }
}
