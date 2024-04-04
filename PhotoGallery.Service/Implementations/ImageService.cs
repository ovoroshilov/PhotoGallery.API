using PhotoGallery.DAL.Entities;
using PhotoGallery.DAL.Repositories.Interfaces;
using PhotoGallery.Service.Interfaces;
using PhotoGallery.Service.Models.Request;
using PhotoGallery.Service.Models.Response;

namespace PhotoGallery.Service.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IAlbumRepository _albumRepository;


        public ImageService(IImageRepository imageRepository, IAlbumRepository albumRepository)
        {
            _imageRepository = imageRepository;
            _albumRepository = albumRepository;
        }

        public async Task<ImageResponse> UploadImageAsync(UploadImageRequest request)
        {
            var imageEntity = new ImageEntity
            {
                Id = Guid.NewGuid(),
                Path = request.Path,
                Name = request.Name,
                Url = request.Url,
                FileExtension = request.Extension,
                CreatedAt = DateTime.Now,
                Album = new AlbumEntity()
            };
            var existingAlbum = await _albumRepository.GetById(request.AlbumId);
            if (existingAlbum is not null)
            {
                imageEntity.Album = existingAlbum;
            }
            try
            {
                var dbResponse = await _imageRepository.CreateImageAsync(imageEntity);
                var response = new ImageResponse
                {
                    Id = dbResponse.Id,
                    Path = dbResponse.Path,
                    Name = dbResponse.Name,
                    Url = dbResponse.Url,
                    CreatedAt = DateTime.Now
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ImageResponse>> GetImagesPage(PaginationImageRequest request)
        {
            var images = await _imageRepository.GetImagePage(request.Page, request.PageSize, request.AlbumId);
            return images.Select(i => new ImageResponse
            {
                Id = i.Id,
                Name = i.Name,
                Path = i.Path,
                Url = i.Url,
                CreatedAt = i.CreatedAt
            });
        }
    }
}
