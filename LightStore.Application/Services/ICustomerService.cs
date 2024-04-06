using LightStore.Application.Dtos.Customer;
using System;
using System.Threading.Tasks;

namespace LightStore.Application.Services
{
    public interface ICustomerService
    {
        Task<CustomerVm> Get(Guid id);
        Task<CustomerAppUserVm> Create(CreateCustomerDto dto);
        Task<UpdateCustomerVm> Update(Guid AppUserId, UpdateCustomerDto dto);
    }
}
