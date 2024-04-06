using LightStore.Application.Dtos.Employee;
using LightStore.Application.Services;
using LightStore.Persistence.Entities.Base;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LightStore.Application
{
    public static class DbInitializer
    {
        private static readonly CreateEmployeeDto defaultAdminDto = new() // TODO: Secure credentials.
        {
            Email = "noreply@default.admin",
            Password = "admin",
            Surname = "Default",
            Name = "Admin",
            Role = AppUserRole.Admin,
        };

        public static async Task Initialize(ILightStoreDbContext dbContext, IEmployeeService employeeService)
        {
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var defaultAdminExists = await dbContext.AppUsers
                .AnyAsync(au => au.Role == AppUserRole.Admin && au.Email == defaultAdminDto.Email);
            if (defaultAdminExists) return;

            await employeeService.Create(defaultAdminDto);
        }
    }
}
