using LightStore.Application.Dtos.DeliveryInformation;
using LightStore.Application.Dtos.ProductInOrder;
using LightStore.Application.Dtos.Warehouse;
using LightStore.Persistence.Entities.Base;
using System;
using System.Collections.Generic;

namespace LightStore.Application.Dtos.Order
{
    public class CustomersOrderDetailsVm
    {
        public WarehouseVm Warehouse { get; set; }
        public DeliveryInformationVm DeliveryInformation { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ProductInOrderVm> ProductsInOrder { get; set; }
    }
}
