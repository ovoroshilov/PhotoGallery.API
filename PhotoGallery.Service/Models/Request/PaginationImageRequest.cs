namespace PhotoGallery.Service.Models.Request
{
    public class PaginationImageRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; } = 5;
        public Guid AlbumId { get; set; }
    }
}
