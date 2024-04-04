namespace PhotoGallery.DAL.Entities
{
    public class AlbumEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt {  get; set; }
        public List<ImageEntity> Images { get; set; } = [];
    }
}