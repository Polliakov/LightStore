using LightStore.Application;
using LightStore.Application.Services;
using LightStore.Persistence.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace LightStore.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            
            var serviceProvider = scope.ServiceProvider;
            //try
            //{
            var dbContext = serviceProvider.GetRequiredService<ILightStoreDbContext>();
            var employeeService = serviceProvider.GetRequiredService<IEmployeeService>();
            await DbInitializer.Initialize(dbContext, employeeService);
            //}
            //catch (Exception ex)
            //{

            //}
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options =>
                    {

                    });
                });
    }
}
