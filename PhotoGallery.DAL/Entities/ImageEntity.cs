namespace PhotoGallery.DAL.Entities
{
    public class ImageEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Url { get; set; } = string.Empty;
        public string FileExtension {  get; set; } = string.Empty;
        public Guid AlbumId { get; set; }
        public AlbumEntity? Album { get; set; }
    }
}
