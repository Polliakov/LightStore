using LightStore.Application.Dtos.Employee;
using LightStore.Application.Exceptions;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightStore.Application.Services.Implementations
{
    public class EmployeeService : BaseDataService, IEmployeeService
    {
        public EmployeeService(ILightStoreDbContext dbContext,
                               IAppUserService appUserService,
                               IEmployeeMapper employeeMapper)
            : base(dbContext)
        {
            this.appUserService = appUserService;
            this.employeeMapper = employeeMapper;
        }

        private readonly IAppUserService appUserService;
        private readonly IEmployeeMapper employeeMapper;

        public async Task<EmployeeVm> Get(Guid appUserId)
        {
            var employee = await dbContext.Employees.AsNoTracking()
                .FirstOrDefaultAsync(e => e.AppUserId == appUserId)
                ?? throw new NotFoundException(nameof(Employee), appUserId);
            return employeeMapper.MapToDto(employee);
        }

        public async Task<List<GetAllEmployeeVm>> GetAll()
        {
            return await dbContext.Employees.AsNoTracking()
                .Include(e => e.AppUser)
                .Select(employeeMapper.ProjectToDtos).ToListAsync();
        }

        public async Task<Guid> Create(CreateEmployeeDto employeeDto)
        {
            var employee = employeeMapper.MapFromDto(employeeDto);
            var createAppUserDto = employeeMapper.Map(employeeDto);

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var appUser = await appUserService.Create(createAppUserDto);
                employee.AppUserId = appUser.AppUserId;

                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return employee.AppUserId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task Update(UpdateEmployeeDto employeeDto)
        {
            var employeeExists = await dbContext.Employees.AnyAsync(e => e.AppUserId == employeeDto.AppUserId);
            if (!employeeExists)
                throw new NotFoundException(nameof(Employee), employeeDto.AppUserId);

            var employee = employeeMapper.MapFromDto(employeeDto);
            var updateAppUserDto = employeeMapper.Map(employeeDto);

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await appUserService.Update(updateAppUserDto);
                dbContext.Employees.Update(employee);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task Delete(Guid AppUserId)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.AppUserId == AppUserId)
                ?? throw new NotFoundException(nameof(Employee), AppUserId);

            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await appUserService.Delete(employee.AppUserId);
                employee.Deleted = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
