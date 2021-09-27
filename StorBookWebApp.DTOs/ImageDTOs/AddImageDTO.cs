using Microsoft.AspNetCore.Http;

namespace StorBookWebApp.DTOs.ImageDTOs
{
    public class AddImageDto
    {
        public IFormFile Image { get; set; }
    }
}
