using Moq;
using PhotoGallery.DAL.Entities;
using PhotoGallery.DAL.Repositories.Interfaces;
using PhotoGallery.Service.Implementations;
using PhotoGallery.Service.Models.Request;

namespace PhotoGallery.Tests.Services
{
    public class ImageServiceTests
    {
        [Fact]
        public async Task UploadUmageAsync_ReturnImageResponse()
        {
            var mockImgRepo = new Mock<IImageRepository>();
            var mockAlbumRepo = new Mock<IAlbumRepository>();

            var imageService = new ImageService(mockImgRepo.Object, mockAlbumRepo.Object);

            var request = new UploadImageRequest
            {
                Path = "testPath",
                Name = "testName",
                Url = "testUrl",
                Extension = ".jpg",
                AlbumId = Guid.NewGuid()
            };

            mockAlbumRepo.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                          .ReturnsAsync(new AlbumEntity());
            mockImgRepo.Setup(mockImgRepo => mockImgRepo.CreateImageAsync(It.IsAny<ImageEntity>()))
            .ReturnsAsync(new ImageEntity
            {
                Id = Guid.NewGuid(),
                Path = request.Path,
                Name = request.Name,
                Url = request.Url,
                FileExtension = request.Extension,
                CreatedAt = DateTime.Now
            });

            var result = await imageService.UploadImageAsync(request);

            Assert.NotNull(result);
            Assert.Equal(request.Path, result.Path);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Url, result.Url);
        }

        [Fact]
        public async Task GetImagesPage_ValidRequest_ReturnsListOfImageResponses()
        {
            var mockImageRepository = new Mock<IImageRepository>();
            var mockAlbumRepository = new Mock<IAlbumRepository>();

            var imageService = new ImageService(mockImageRepository.Object, mockAlbumRepository.Object);

            var request = new PaginationImageRequest
            {
                Page = 1,
                PageSize = 10,
                AlbumId = Guid.NewGuid()
            };

            var mockImages = new List<ImageEntity>
        {
            new ImageEntity
            {
                Id = Guid.NewGuid(),
                Path = "testPath1",
                Name = "testName1",
                Url = "testUrl1",
                CreatedAt = DateTime.Now
            },
            new ImageEntity
            {
                Id = Guid.NewGuid(),
                Path = "testPath2",
                Name = "testName2",
                Url = "testUrl2",
                CreatedAt = DateTime.Now
            }
        };

            mockImageRepository.Setup(repo => repo.GetImagePage(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>()))
                               .ReturnsAsync(mockImages);

            var result = await imageService.GetImagesPage(request);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(mockImages[0].Id, result.First().Id);
            Assert.Equal(mockImages[0].Path, result.First().Path);
            Assert.Equal(mockImages[0].Name, result.First().Name);
            Assert.Equal(mockImages[0].Url, result.First().Url);
            Assert.Equal(mockImages[0].CreatedAt, result.First().CreatedAt);
        }
    }
}
