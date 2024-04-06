using LightStore.Persistence.Interfaces;
using LightStore.Persistence.RawSql;
using LightStore.Persistence.RawSql.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightStore.Persistence
{
    public static class DependencyEnjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MsSqlDbConnection");
            services.AddDbContext<LightStoreDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ILightStoreDbContext>(provider => provider.GetService<LightStoreDbContext>());
            services.AddScoped<IRawSqlProvider, MsSqlRawSqlProvider>();
            return services;
        }
    }
}
