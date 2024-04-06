using System;
using System.Linq.Expressions;
using LightStore.Application.Dtos.AppUser;
using LightStore.Application.Dtos.Employee;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class EmployeeMapper : IEmployeeMapper
    {
        public Expression<Func<Employee, GetAllEmployeeVm>> ProjectToDtos => p1 => new GetAllEmployeeVm()
        {
            AppUserId = p1.AppUserId,
            Email = p1.AppUser.Email,
            Role = p1.AppUser.Role,
            Surname = p1.Surname,
            Name = p1.Name,
            Patronymic = p1.Patronymic
        };
        public EmployeeVm MapToDto(Employee p2)
        {
            return p2 == null ? null : new EmployeeVm()
            {
                Surname = p2.Surname,
                Name = p2.Name,
                Patronymic = p2.Patronymic
            };
        }
        public Employee MapFromDto(CreateEmployeeDto p3)
        {
            return p3 == null ? null : new Employee()
            {
                Surname = p3.Surname,
                Name = p3.Name,
                Patronymic = p3.Patronymic
            };
        }
        public Employee MapFromDto(UpdateEmployeeDto p4)
        {
            return p4 == null ? null : new Employee()
            {
                AppUserId = p4.AppUserId,
                Surname = p4.Surname,
                Name = p4.Name,
                Patronymic = p4.Patronymic
            };
        }
        public CreateAppUserDto Map(CreateEmployeeDto p5)
        {
            return p5 == null ? null : new CreateAppUserDto()
            {
                Email = p5.Email,
                Password = p5.Password,
                Role = p5.Role
            };
        }
        public UpdateAppUserDto Map(UpdateEmployeeDto p6)
        {
            return p6 == null ? null : new UpdateAppUserDto()
            {
                AppUserId = p6.AppUserId,
                Email = p6.Email,
                Password = p6.Password,
                Role = p6.Role
            };
        }
    }
}