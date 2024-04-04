namespace PhotoGallery.Service.Models.Request
{
    public class PaginationAlbumRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; } = 5;
    }
}
