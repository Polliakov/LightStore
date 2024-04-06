using System.Collections.Generic;
using LightStore.Application.Dtos.Customer;
using LightStore.Application.Dtos.DeliveryInformation;
using LightStore.Application.Dtos.Order;
using LightStore.Application.Dtos.ProductInOrder;
using LightStore.Application.Dtos.Warehouse;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class OrderMapper : IOrderMapper
    {
        public OrderDetailsVm MapToDetailsDto(Order p1)
        {
            return p1 == null ? null : new OrderDetailsVm()
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
                CreationDate = p1.CreationDate,
                ProductsInOrder = funcMain1(p1.ProductsInOrder)
            };
        }
        public CustomersOrderDetailsVm MapToCustomersDetailsDto(Order p3)
        {
            return p3 == null ? null : new CustomersOrderDetailsVm()
            {
                Warehouse = p3.Warehouse == null ? null : new WarehouseVm()
                {
                    Id = p3.Warehouse.Id,
                    Name = p3.Warehouse.Name,
                    Address = p3.Warehouse.Address,
                    PhoneNumber = p3.Warehouse.PhoneNumber
                },
                DeliveryInformation = p3.DeliveryInformation == null ? null : new DeliveryInformationVm() {Id = p3.DeliveryInformation.Id},
                Status = p3.Status,
                CreationDate = p3.CreationDate,
                ProductsInOrder = funcMain2(p3.ProductsInOrder)
            };
        }
        public OrderVm MapToDto(Order p5)
        {
            return p5 == null ? null : new OrderVm()
            {
                Id = p5.Id,
                Warehouse = p5.Warehouse == null ? null : new WarehouseMinifiedVm()
                {
                    Id = p5.Warehouse.Id,
                    Name = p5.Warehouse.Name
                },
                DeliveryInformation = p5.DeliveryInformation == null ? null : new DeliveryInformationVm() {Id = p5.DeliveryInformation.Id},
                Status = p5.Status,
                CreationDate = p5.CreationDate
            };
        }
        public Order MapFromDto(CreateOrderDto p6)
        {
            return p6 == null ? null : new Order()
            {
                WarehouseId = p6.WarehouseId,
                DeliveryInformation = p6.DeliveryInformation == null ? null : new DeliveryInformation()
                {
                    Address = p6.DeliveryInformation.Address,
                    PhoneNumber = p6.DeliveryInformation.PhoneNumber
                },
                ProductsInOrder = funcMain3(p6.ProductsInOrder)
            };
        }
        
        private List<ProductInOrderVm> funcMain1(List<ProductInOrder> p2)
        {
            if (p2 == null)
            {
                return null;
            }
            List<ProductInOrderVm> result = new List<ProductInOrderVm>(p2.Count);
            
            int i = 0;
            int len = p2.Count;
            
            while (i < len)
            {
                ProductInOrder item = p2[i];
                result.Add(item == null ? null : new ProductInOrderVm()
                {
                    ProductId = item.ProductId,
                    Title = item.Product == null ? null : item.Product.Title,
                    Price = item.Price,
                    Count = item.Count
                });
                i++;
            }
            return result;
            
        }
        
        private List<ProductInOrderVm> funcMain2(List<ProductInOrder> p4)
        {
            if (p4 == null)
            {
                return null;
            }
            List<ProductInOrderVm> result = new List<ProductInOrderVm>(p4.Count);
            
            int i = 0;
            int len = p4.Count;
            
            while (i < len)
            {
                ProductInOrder item = p4[i];
                result.Add(item == null ? null : new ProductInOrderVm()
                {
                    ProductId = item.ProductId,
                    Title = item.Product == null ? null : item.Product.Title,
                    Price = item.Price,
                    Count = item.Count
                });
                i++;
            }
            return result;
            
        }
        
        private List<ProductInOrder> funcMain3(List<CreateProductInOrderDto> p7)
        {
            if (p7 == null)
            {
                return null;
            }
            List<ProductInOrder> result = new List<ProductInOrder>(p7.Count);
            
            int i = 0;
            int len = p7.Count;
            
            while (i < len)
            {
                CreateProductInOrderDto item = p7[i];
                result.Add(item == null ? null : new ProductInOrder()
                {
                    ProductId = item.ProductId,
                    Count = item.Count
                });
                i++;
            }
            return result;
            
        }
    }
}