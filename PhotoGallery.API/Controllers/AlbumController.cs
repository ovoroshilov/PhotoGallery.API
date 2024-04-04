using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Service.Interfaces;
using PhotoGallery.Service.Models.Request;

namespace PhotoGallery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums([FromQuery] PaginationAlbumRequest request)
        {
            var response = await _albumService.GetAlbumsPage(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody]CreateAlbumRequest request)
        {
            var response = await _albumService.CreateAlbumAsync(request);
            return Ok(response);
        }
    }
}
