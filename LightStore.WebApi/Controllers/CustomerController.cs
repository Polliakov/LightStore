using LightStore.Application.Dtos.Customer;
using LightStore.Application.Services;
using LightStore.Persistence.Entities.Base;
using LightStore.WebApi.Attributes;
using LightStore.WebApi.Dtos.Customer;
using LightStore.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LightStore.WebApi.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(ICustomerService customerService,
                                  ITokenService tokenService)
        {
            this.customerService = customerService;
            this.tokenService = tokenService;
        }

        private readonly ICustomerService customerService;
        private readonly ITokenService tokenService;

        [HttpGet("Authed")]
        [AuthorizeRole(AppUserRole.Customer)]
        public async Task<ActionResult<CustomerVm>> GetAuthorized()
        {
            var vm = await customerService.Get(UserId);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerSignUpResponse>> SignUp([FromBody] CreateCustomerDto customerDto)
        {
            var vm = await customerService.Create(customerDto);
            var token = tokenService.GenerateToken(vm.AppUser);
            var response = new CustomerSignUpResponse
            {
                AppUser = vm.AppUser,
                Customer = vm.Customer,
                Token = token
            };
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeRole(AppUserRole.Customer)]
        public async Task<ActionResult<UpdateCustomerVm>> Update([FromBody] UpdateCustomerDto dto)
        {
            var vm = await customerService.Update(UserId, dto);
            return Ok(vm);
        }
    }
}
