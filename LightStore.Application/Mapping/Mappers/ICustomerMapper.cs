using LightStore.Application.Dtos.AppUser;
using LightStore.Application.Dtos.Customer;
using LightStore.Persistence.Entities;
using Mapster;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface ICustomerMapper
    {
        Customer MapFromDto(CreateCustomerDto customerDto);
        CustomerVm MapToDto(Customer customer);
        UpdateCustomerVm MapToUpdateDto(Customer customer);
        CustomerAppUserVm MapToDto((Customer, AppUserVm) src);
        CreateAppUserDto Map(CreateCustomerDto customerDto);
    }
}
