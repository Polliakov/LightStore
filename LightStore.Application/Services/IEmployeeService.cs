using LightStore.Application.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeVm> Get(Guid appUserId);
        Task<List<GetAllEmployeeVm>> GetAll();
        Task<Guid> Create(CreateEmployeeDto employeeDto);
        Task Update(UpdateEmployeeDto employeeDto);
        Task Delete(Guid appUserId);
    }
}
