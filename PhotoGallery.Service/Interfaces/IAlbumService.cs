using PhotoGallery.DAL.Entities;
using PhotoGallery.Service.Models.Request;
using PhotoGallery.Service.Models.Response;

namespace PhotoGallery.Service.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumResponse>> GetAlbumsPage(PaginationAlbumRequest request);
        Task<AlbumResponse> CreateAlbumAsync(CreateAlbumRequest album);
        Task<AlbumEntity?> DeleteAlbumAsync(AlbumEntity album);
    }
}
