using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Interfaces
{
    public interface IImageService
    {
        Task<UploadResult> UploadImageAsync(IFormFile image);
    }
}
