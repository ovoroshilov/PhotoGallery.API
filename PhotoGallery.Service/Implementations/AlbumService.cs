using Microsoft.Extensions.Logging;
using PhotoGallery.DAL.Entities;
using PhotoGallery.DAL.Repositories.Interfaces;
using PhotoGallery.Service.Interfaces;
using PhotoGallery.Service.Models.Request;
using PhotoGallery.Service.Models.Response;

namespace PhotoGallery.Service.Implementations;

public class AlbumService : IAlbumService
{
    public readonly IAlbumRepository _albumRepository;
    private readonly ILogger _logger;
    public AlbumService(IAlbumRepository albumRepository, ILogger<AlbumService> logger)
    {
        _albumRepository = albumRepository;
        _logger = logger;
    }

    public async Task<AlbumResponse> CreateAlbumAsync(CreateAlbumRequest album)
    {
        var albumEntity = new AlbumEntity
        {
            Id = Guid.NewGuid(),
            Name = album.Name.Trim(),
            CreatedAt = DateTime.Now
        };
        try
        {
            var entity = await _albumRepository.CreateAlbumAsync(albumEntity);
            var response = new AlbumResponse
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            throw new Exception("Failed to create the album entity", ex);
        }
    }

    public Task<AlbumEntity?> DeleteAlbumAsync(AlbumEntity album)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AlbumResponse>> GetAlbumsPage(PaginationAlbumRequest request)
    {
        try
        {
            var albums = await _albumRepository.GetAlbumsPage(request.Page, request.PageSize);
            var albumsResponse = new List<AlbumResponse>();

            foreach (var album in albums)
            {
                albumsResponse.Add(new AlbumResponse
                {
                    Id = album.Id,
                    Name = album.Name,
                    FirstImage = album.Images
                    .OrderBy(i => i.CreatedAt)
                    .Select(x => new ImageResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Path = x.Path,
                        Url = x.Url
                    })
                    .FirstOrDefault(new ImageResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = "Not found",
                        CreatedAt = DateTime.Now,
                        Path = "/path",
                        Url = "https://i.imgur.com/Mxg3dwB.jpeg"
                    })
                });
            }
            return albumsResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            throw new Exception("An error occured while getting albums", ex);
        }
    }
}
