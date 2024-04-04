namespace PhotoGallery.Service.Models.Response
{
    public class ImageResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
