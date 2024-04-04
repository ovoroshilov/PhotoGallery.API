namespace PhotoGallery.Service.Models.Response
{
    public class AlbumResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ImageResponse? FirstImage { get; set; }
    }
}
