using LightStore.Application.Dtos.Employee;
using LightStore.Application.Services;
using LightStore.Persistence.Entities.Base;
using LightStore.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class EmployeeController : BaseController
    {
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        private readonly IEmployeeService employeeService;

        [HttpGet]
        [AuthorizeRole(AppUserRole.Admin)]
        public async Task<ActionResult<List<GetAllEmployeeVm>>> GetAll()
        {
            var employees = await employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("Authed")]
        [AuthorizeRole(AppUserRole.Admin, AppUserRole.Employee)]
        public async Task<ActionResult<EmployeeVm>> GetAuthorized()
        {
            var vm = await employeeService.Get(UserId);
            return Ok(vm);
        }

        [HttpPost]
        [AuthorizeRole(AppUserRole.Admin)]
        public async Task<ActionResult<Guid>> SignUp(CreateEmployeeDto employeeDto)
        {
            var id = await employeeService.Create(employeeDto);
            return Ok(id);
        }

        [HttpPut]
        [AuthorizeRole(AppUserRole.Employee)]
        public async Task<IActionResult> Update(UpdateEmployeeDto employeeDto)
        {
            await employeeService.Update(employeeDto);
            return NoContent();
        }

        [HttpDelete("{id}")] 
        [AuthorizeRole(AppUserRole.Admin)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await employeeService.Delete(Id);
            return NoContent();
        }
    }
}
