namespace PhotoGallery.Service.Models.Request
{
    public class UploadImageRequest
    {
        public string Path { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Extension {  get; set; } = string.Empty;
        public Guid AlbumId { get; set; }
    }
}
