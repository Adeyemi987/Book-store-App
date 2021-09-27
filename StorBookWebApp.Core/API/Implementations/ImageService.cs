using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StorBookWebApp.Core.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.API.Implementations
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        public ImageService(Cloudinary cloudinary, IConfiguration configuration)
        {
            _cloudinary = cloudinary;
            _configuration = configuration;
        }
        public async Task<UploadResult> UploadImageAsync(IFormFile image)
        {
            var isCorrectFormat = false;
            var photoSettings = _configuration.GetSection("PhotoSettings");
            var imageExtensionList = photoSettings.GetSection("Formats").Get<List<string>>();

            var imageMaxSize = Convert.ToInt32(photoSettings.GetSection("Size").Get<string>());

            if (image.Length > imageMaxSize)
                throw new ArgumentOutOfRangeException(imageMaxSize.ToString(),
                    image.Length.ToString(), "Maximum Image size required is 3mb");

            foreach (var item in imageExtensionList)
            {
                if (image.FileName.EndsWith(item))
                {
                    isCorrectFormat = true;
                    break;
                }
            }

            if (isCorrectFormat == false)
                throw new ArgumentException("File format not supported");

            var uploadResult = new ImageUploadResult();
            using (var imageStream = image.OpenReadStream())
            {
                string fileName = Guid.NewGuid().ToString() + image.FileName;

                uploadResult = await _cloudinary.UploadAsync(new ImageUploadParams
                {
                    File = new FileDescription(fileName, imageStream),
                    Transformation = new Transformation().Radius("max").Chain().Crop("scale").Width(200).Height(200)
                });
            }

            return uploadResult;
        }
    }
}
