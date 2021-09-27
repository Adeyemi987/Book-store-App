using Newtonsoft.Json;

namespace StorBookWebApp.DTOs.ImageDTOs
{
    public class ImageUploadDTO
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
