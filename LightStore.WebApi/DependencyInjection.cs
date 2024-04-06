using LightStore.WebApi.Mapping.Mappers;
using LightStore.WebApi.Mapping.Mappers.Implementations;
using LightStore.WebApi.Services;
using LightStore.WebApi.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace LightStore.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddSingleton<IProductMapper, ProductMapper>();
            services.AddSingleton<ICartMapper, CartMapper>();
            services.AddSingleton<IProductsCategoryMapper, ProductsCategoryMapper>();
            services.AddSingleton<IWarehouseMapper, WarehouseMapper>();
            services.AddSingleton<IProductsAddingMapper, ProductsAddingMapper>();
            services.AddSingleton<IProductInAddingMapper, ProductInAddingMapper>();
            services.AddSingleton<IProductInOrderMapper, ProductInOrderMapper>();
            services.AddSingleton<IOrderMapper, OrderMapper>();

            services.AddScoped<ITokenService, JwtService>();

            return services;
        }
    }
}
