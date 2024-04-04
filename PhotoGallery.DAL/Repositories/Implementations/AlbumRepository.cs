using Microsoft.EntityFrameworkCore;
using PhotoGallery.DAL.Contexts;
using PhotoGallery.DAL.Entities;
using PhotoGallery.DAL.Repositories.Interfaces;

namespace PhotoGallery.DAL.Repositories.Implementations
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly GalleryDbContext _dbContext;

        public AlbumRepository(GalleryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AlbumEntity> CreateAlbumAsync(AlbumEntity album)
        {
            await _dbContext.Albums.AddAsync(album);
            await _dbContext.SaveChangesAsync();

            return album;
        }

        public async Task<AlbumEntity?> DeleteAlbumAsync(AlbumEntity album)
        {
            var existingAlbum = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == album.Id);
            if (existingAlbum is not null)
            {
                _dbContext.Albums.Remove(existingAlbum);
                await _dbContext.SaveChangesAsync();
                return existingAlbum;
            }
            return null;
        }

        public async Task<IEnumerable<AlbumEntity>> GetAlbumsPage(int page, int pageSize)
        {
            return await _dbContext.Albums
                .AsNoTracking()
                .Include(a => a.Images)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<AlbumEntity?> GetById(Guid id)
        {
            return await _dbContext.Albums
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<AlbumEntity?> UpdateAlbumAsync(AlbumEntity album)
        {
            var existingAlbum = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == album.Id);
            if (existingAlbum is not null)
            {
                _dbContext.Entry(existingAlbum).CurrentValues.SetValues(album);
                await _dbContext.SaveChangesAsync();
                return existingAlbum;
            }
            return null;
        }
    }
}
