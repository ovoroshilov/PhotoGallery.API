using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Service.Interfaces;
using PhotoGallery.Service.Models.Request;

namespace PhotoGallery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;

        public ImageController(IImageService imageService, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages([FromQuery] PaginationImageRequest request)
        {
            var response = await _imageService.GetImagesPage(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm]IFormFile file, [FromForm]Guid albumId, [FromForm]string fileName)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{fileName}{Path.GetExtension(file.FileName)}");
                using var stream = new FileStream(localPath, FileMode.Create);
                await file.CopyToAsync(stream);

                var request = GenerateUrl(localPath, file, albumId, fileName);
                var response = await _imageService.UploadImageAsync(request);
                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        private UploadImageRequest GenerateUrl(string localPath, IFormFile file, Guid albumId, string fileName) 
        {
            var request = _contextAccessor.HttpContext.Request;
            var urlPath = $"{request.Scheme}://{request.Host}{request.PathBase}/Images/{fileName}{Path.GetExtension(file.FileName)}";
            var requestToUpload = new UploadImageRequest
            {
                Path = localPath,
                Url = urlPath,
                Name = fileName,
                Extension = Path.GetExtension(localPath),
                AlbumId = albumId
            };
            return requestToUpload;
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }
            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more then 10MB");
            }
        }
    }
}
