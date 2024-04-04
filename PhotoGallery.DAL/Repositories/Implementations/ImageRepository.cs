using Microsoft.EntityFrameworkCore;
using PhotoGallery.DAL.Contexts;
using PhotoGallery.DAL.Entities;
using PhotoGallery.DAL.Repositories.Interfaces;

namespace PhotoGallery.DAL.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly GalleryDbContext _dbContext;

        public ImageRepository(GalleryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ImageEntity> CreateImageAsync(ImageEntity image)
        {
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }

        public async Task<ImageEntity?> DeleteImageAsync(ImageEntity image)
        {
            var existingImage = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == image.Id);
            if (existingImage is not null)
            {
                _dbContext.Images.Remove(existingImage);
                await _dbContext.SaveChangesAsync();
                return existingImage;
            }
            return null;
        }

        public async Task<IEnumerable<ImageEntity>> GetImagePage(int page, int pageSize, Guid albumId)
        {
            return await _dbContext
                .Images
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .Where(i => i.AlbumId == albumId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<ImageEntity?> GetById(Guid id)
        {
            return await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ImageEntity?> UpdateImageAsync(ImageEntity image)
        {
            var existingImage = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == image.Id);
            if (existingImage is not null)
            {
                _dbContext.Entry(existingImage).CurrentValues.SetValues(image);
                await _dbContext.SaveChangesAsync();
                return existingImage;
            }
            return null;
        }
    }
}
