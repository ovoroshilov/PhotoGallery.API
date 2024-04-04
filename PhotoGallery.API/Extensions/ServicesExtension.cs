using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhotoGallery.DAL.Contexts;
using PhotoGallery.DAL.Repositories.Implementations;
using PhotoGallery.DAL.Repositories.Interfaces;
using PhotoGallery.Service.Implementations;
using PhotoGallery.Service.Interfaces;

namespace PhotoGallery.API.Extensions
{
    public static class ServicesExtension
    {
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GalleryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PhotoGalleryConnectionString"));
            });
        }
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
