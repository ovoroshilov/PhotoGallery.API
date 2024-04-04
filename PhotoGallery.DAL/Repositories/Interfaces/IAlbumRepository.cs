using PhotoGallery.DAL.Entities;

namespace PhotoGallery.DAL.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<AlbumEntity>> GetAlbumsPage(int page, int pageSize);
        Task<AlbumEntity?> GetById(Guid id);
        Task<AlbumEntity> CreateAlbumAsync(AlbumEntity album);
        Task<AlbumEntity?> DeleteAlbumAsync(AlbumEntity album);
        Task<AlbumEntity?> UpdateAlbumAsync(AlbumEntity album);
    }
}
