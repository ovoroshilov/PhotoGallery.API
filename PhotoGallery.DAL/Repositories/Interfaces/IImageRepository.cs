using PhotoGallery.DAL.Entities;

namespace PhotoGallery.DAL.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageEntity>> GetImagePage(int page, int pageSize, Guid albumId);
        Task<ImageEntity?> GetById(Guid id);
        Task<ImageEntity> CreateImageAsync(ImageEntity image);
        Task<ImageEntity?> DeleteImageAsync(ImageEntity image);
        Task<ImageEntity?> UpdateImageAsync(ImageEntity image);
    }
}
