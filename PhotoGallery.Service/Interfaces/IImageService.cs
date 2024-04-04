using PhotoGallery.Service.Models.Request;
using PhotoGallery.Service.Models.Response;

namespace PhotoGallery.Service.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageResponse>> GetImagesPage(PaginationImageRequest request);
        Task<ImageResponse> UploadImageAsync(UploadImageRequest request);
    }
}
