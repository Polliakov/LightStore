using LightStore.Application.Dtos.AppUser;
using LightStore.Application.Dtos.Employee;
using LightStore.Persistence.Entities;
using Mapster;
using System;
using System.Linq.Expressions;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface IEmployeeMapper
    {
        Expression<Func<Employee, GetAllEmployeeVm>> ProjectToDtos { get; }
        EmployeeVm MapToDto(Employee employee);
        Employee MapFromDto(CreateEmployeeDto employeeDto);
        Employee MapFromDto(UpdateEmployeeDto employeeDto);
        CreateAppUserDto Map(CreateEmployeeDto employeeDto);
        UpdateAppUserDto Map(UpdateEmployeeDto employeeDto);
    }
}
