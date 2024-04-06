using System;
using LightStore.Application.Dtos.AppUser;
using LightStore.Application.Dtos.Customer;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class CustomerMapper : ICustomerMapper
    {
        public Customer MapFromDto(CreateCustomerDto p1)
        {
            return p1 == null ? null : new Customer()
            {
                CartId = p1.CartId == null ? default(Guid) : (Guid)p1.CartId,
                Surname = p1.Surname,
                Name = p1.Name,
                Patronymic = p1.Patronymic
            };
        }
        public CustomerVm MapToDto(Customer p2)
        {
            return p2 == null ? null : new CustomerVm()
            {
                Surname = p2.Surname,
                Name = p2.Name,
                Patronymic = p2.Patronymic,
                CartId = p2.CartId
            };
        }
        public UpdateCustomerVm MapToUpdateDto(Customer p3)
        {
            return p3 == null ? null : new UpdateCustomerVm()
            {
                Surname = p3.Surname,
                Name = p3.Name,
                Patronymic = p3.Patronymic
            };
        }
        public CustomerAppUserVm MapToDto(ValueTuple<Customer, AppUserVm> p4)
        {
            return new CustomerAppUserVm()
            {
                AppUser = new AppUserVm()
                {
                    AppUserId = p4.Item2.AppUserId,
                    Email = p4.Item2.Email,
                    Role = p4.Item2.Role
                },
                Customer = new CustomerVm()
                {
                    Surname = p4.Item1.Surname,
                    Name = p4.Item1.Name,
                    Patronymic = p4.Item1.Patronymic,
                    CartId = p4.Item1.CartId
                }
            };
        }
        public CreateAppUserDto Map(CreateCustomerDto p5)
        {
            return p5 == null ? null : new CreateAppUserDto()
            {
                Email = p5.Email,
                Password = p5.Password
            };
        }
    }
}