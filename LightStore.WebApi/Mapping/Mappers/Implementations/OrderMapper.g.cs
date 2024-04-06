using LightStore.Application.Dtos.Customer;
using LightStore.Application.Dtos.DeliveryInformation;
using LightStore.Application.Dtos.Order;
using LightStore.Application.Dtos.Warehouse;
using LightStore.WebApi.Dtos.Order;
using LightStore.WebApi.Mapping.Mappers;

namespace LightStore.WebApi.Mapping.Mappers.Implementations
{
    public partial class OrderMapper : IOrderMapper
    {
        public OrderDetailsResponse MapToDetailsResponse(OrderDetailsVm p1)
        {
            return p1 == null ? null : new OrderDetailsResponse()
            {
                Warehouse = p1.Warehouse == null ? null : new WarehouseVm()
                {
                    Id = p1.Warehouse.Id,
                    Name = p1.Warehouse.Name,
                    Address = p1.Warehouse.Address,
                    PhoneNumber = p1.Warehouse.PhoneNumber
                },
                DeliveryInformation = p1.DeliveryInformation == null ? null : new DeliveryInformationVm() {Id = p1.DeliveryInformation.Id},
                Customer = p1.Customer == null ? null : new RecipientVm()
                {
                    Surname = p1.Customer.Surname,
                    Name = p1.Customer.Name,
                    Patronymic = p1.Customer.Patronymic
                },
                Status = p1.Status,
                CreationDate = p1.CreationDate
            };
        }
        public CustomersOrderDetailsResponse MapToCustomersDetailsResponse(CustomersOrderDetailsVm p2)
        {
            return p2 == null ? null : new CustomersOrderDetailsResponse()
            {
                Warehouse = p2.Warehouse == null ? null : new WarehouseVm()
                {
                    Id = p2.Warehouse.Id,
                    Name = p2.Warehouse.Name,
                    Address = p2.Warehouse.Address,
                    PhoneNumber = p2.Warehouse.PhoneNumber
                },
                DeliveryInformation = p2.DeliveryInformation == null ? null : new DeliveryInformationVm() {Id = p2.DeliveryInformation.Id},
                Status = p2.Status,
                CreationDate = p2.CreationDate
            };
        }
    }
}