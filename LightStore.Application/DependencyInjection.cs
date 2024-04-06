using LightStore.Application.Mapping.Mappers.Implementations;
using LightStore.Application.Mapping.Mappers;
using LightStore.Application.Services;
using LightStore.Application.Services.Implementations;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace LightStore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IAppUserMapper, AppUserMapper>();
            services.AddSingleton<IProductMapper, ProductMapper>();
            services.AddSingleton<IProductsCategoryMapper, ProductsCategoryMapper>();
            services.AddSingleton<IEmployeeMapper, EmployeeMapper>();
            services.AddSingleton<ICustomerMapper, CustomerMapper>();
            services.AddSingleton<IWarehouseMapper, WarehouseMapper>();
            services.AddSingleton<ICartMapper, CartMapper>();
            services.AddSingleton<IProductsAddingMapper, ProductsAddingMapper>();
            services.AddSingleton<IOrderMapper, OrderMapper>();
            services.AddSingleton<IProductInOrderMapper, ProductInOrderMapper>();
            services.AddSingleton<IProductInWarehouseMapper, ProductInWarehouseMapper>();

            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductPriceService, ProductPriceService>();
            services.AddScoped<IProductsCategoryService, ProductsCategoryService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IProductInWarehouseService, ProductInWarehouseService>();
            services.AddScoped<IProductsAddingService, ProductsAddingService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductInOrderService, ProductInOrderService>();

            return services;
        }

        public static IServiceCollection AddMappingConfiguration(this IServiceCollection services, params IRegister[] registers)
        {
            var config = new TypeAdapterConfig();
            foreach (var register in registers)
            {
                register.Register(config);
            }
            services.AddSingleton(config);

            return services;
        }
    }
}
