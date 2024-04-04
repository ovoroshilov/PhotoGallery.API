using Microsoft.EntityFrameworkCore;
using PhotoGallery.DAL.Entities;

namespace PhotoGallery.DAL.Contexts
{
    public class GalleryDbContext : DbContext
    {
        public GalleryDbContext(DbContextOptions<GalleryDbContext> options) : base(options)
        {
        }

        public DbSet<AlbumEntity> Albums { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

    }
}
