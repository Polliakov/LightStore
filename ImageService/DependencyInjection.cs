using LightStore.ImageService.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LightStore.ImageService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddImageService(this IServiceCollection services, ImageServiceOptions options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));
            services.AddSingleton<IImageService>(new ImageService(options));

            return services;
        }
    }
}
