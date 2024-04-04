using Microsoft.Extensions.Logging;
using Moq;
using PhotoGallery.DAL.Entities;
using PhotoGallery.DAL.Repositories.Interfaces;
using PhotoGallery.Service.Implementations;
using PhotoGallery.Service.Models.Request;
namespace PhotoGallery.Tests.Services
{
    public class AlbumServiceTests
    {
        [Fact]
        public async Task CreateAlbum_ReturnsAlbumResponse()
        {
            var mockRepository = new Mock<IAlbumRepository>();
            var mockLogger = new Mock<ILogger<AlbumService>>();

            var albumService = new AlbumService(mockRepository.Object, mockLogger.Object);
            var request = new CreateAlbumRequest { Name = "Test Album" };
            var expectedEntity = new AlbumEntity { Id = Guid.NewGuid(), Name = "Test Album" };

            mockRepository.Setup(repo => repo.CreateAlbumAsync(It.IsAny<AlbumEntity>()))
                          .ReturnsAsync(expectedEntity);

            var response = await albumService.CreateAlbumAsync(request);

            Assert.NotNull(response);
            Assert.Equal(expectedEntity.Id, response.Id);
            Assert.Equal(expectedEntity.Name, response.Name);
        }

        [Fact]
        public async Task CreateAlbumAsync_Exception()
        {
            var mockRepository = new Mock<IAlbumRepository>();
            var mockLogger = new Mock<ILogger<AlbumService>>();

            var albumService = new AlbumService(mockRepository.Object, mockLogger.Object);
            var request = new CreateAlbumRequest { Name = "Test Album" };

            mockRepository.Setup(repo => repo.CreateAlbumAsync(It.IsAny<AlbumEntity>()))
                          .ThrowsAsync(new Exception());

            var exception = await Assert.ThrowsAsync<Exception>(async () => await albumService.CreateAlbumAsync(request));
            Assert.Equal("Failed to create the album entity", exception.Message);
        }

        [Fact]
        public async Task GetAlbumsPage_Success()
        {
            var mockRepository = new Mock<IAlbumRepository>();
            var mockLogger = new Mock<ILogger<AlbumService>>();

            var albumService = new AlbumService(mockRepository.Object, mockLogger.Object);
            var paginationRequest = new PaginationAlbumRequest { Page = 1, PageSize = 5 };
            var albums = new List<AlbumEntity> {
            new AlbumEntity
            {
                Id = Guid.NewGuid(),
                Name = "Album 1",
                Images = new List<ImageEntity>
                {
                    new ImageEntity { Id = Guid.NewGuid(), Name = "FirstImage 1", Path = "/path1", Url = "http://url1.com", CreatedAt = DateTime.Now }
                }
            },
            new AlbumEntity
            {
                Id = Guid.NewGuid(),
                Name = "Album 2",
                Images = new List<ImageEntity>
                {
                    new ImageEntity { Id = Guid.NewGuid(), Name = "FirstImage 2", Path = "/path2", Url = "http://url2.com", CreatedAt = DateTime.Now }
                }
            }
        };

            mockRepository.Setup(repo => repo.GetAlbumsPage(paginationRequest.Page, paginationRequest.PageSize))
                          .ReturnsAsync(albums);

            var result = await albumService.GetAlbumsPage(paginationRequest);

            Assert.NotNull(result);
            Assert.Equal(albums.Count, result.Count());
            foreach (var album in albums)
            {
                var responseAlbum = result.FirstOrDefault(a => a.Id == album.Id);
                Assert.NotNull(responseAlbum);
                Assert.Equal(album.Name, responseAlbum.Name);
                Assert.NotNull(responseAlbum.FirstImage);
                Assert.Equal(album.Images.OrderBy(i => i.CreatedAt).First().Id, responseAlbum.FirstImage.Id);
            }
            mockRepository.Verify(repo => repo.GetAlbumsPage(paginationRequest.Page, paginationRequest.PageSize), Times.Once);
        }

        [Fact]
        public async Task GetAlbumsPage_Exception()
        {
            var mockRepository = new Mock<IAlbumRepository>();
            var mockLogger = new Mock<ILogger<AlbumService>>();

            var albumService = new AlbumService(mockRepository.Object, mockLogger.Object);
            var paginationRequest = new PaginationAlbumRequest { Page = 1, PageSize = 10 };
            var exceptionMessage = "Some repository exception";

            mockRepository.Setup(repo => repo.GetAlbumsPage(paginationRequest.Page, paginationRequest.PageSize))
                          .ThrowsAsync(new Exception(exceptionMessage));

            var exception = await Assert.ThrowsAsync<Exception>(async () => await albumService.GetAlbumsPage(paginationRequest));
            Assert.Equal("An error occured while getting albums", exception.Message);
        }
    }
}
 