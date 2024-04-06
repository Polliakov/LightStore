using LightStore.Application.Dtos.AppUser;
using LightStore.Application.Dtos.Cart;
using LightStore.Application.Dtos.Customer;
using LightStore.Application.Dtos.Employee;
using LightStore.Application.Dtos.Order;
using LightStore.Application.Dtos.Product;
using LightStore.Application.Dtos.ProductInAdding;
using LightStore.Application.Dtos.ProductInOrder;
using LightStore.Persistence.Entities;
using Mapster;
using System;

namespace LightStore.Application.Mapping
{
    public class ApplicationMappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Employee, GetAllEmployeeVm>()
                .Map(dest => dest, src => src.AppUser);

            config.NewConfig<ProductInCart, CartItemVm>()
                .Map(dest => dest, src => src.Product);

            config.NewConfig<(Guid cartId, CartItemDto item), ProductInCart>()
                .Map(dest => dest.CartId, src => src.cartId)
                .Map(dest => dest, src => src.item);

            config.NewConfig<(Customer customer, AppUserVm appUser), CustomerAppUserVm>()
                .Map(dest => dest.Customer, src => src.customer)
                .Map(dest => dest.AppUser, src => src.appUser);

            config.NewConfig<CreateAppUserDto, AppUser>()
                .Ignore(dest => dest.Password);

            config.NewConfig<ProductInAdding, ProductInAddingVm>()
                .Map(dest => dest.Title, src => src.Product.Title);

            config.NewConfig<(Product product, int availabilityCount), ProductVm>()
                .Map(dest => dest, src => src.product)
                .Map(dest => dest.AvailabilityCount, src => src.availabilityCount);
            config.NewConfig<(Product product, int availabilityCount), ProductDetailsVm>()
                .Map(dest => dest, src => src.product)
                .Map(dest => dest.AvailabilityCount, src => src.availabilityCount);

            config.NewConfig<ProductInOrder, ProductInOrderVm>()
                .Map(dest => dest.Title, src => src.Product.Title);
        }
    }
}
