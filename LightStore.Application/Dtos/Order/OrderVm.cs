using LightStore.Application.Dtos.DeliveryInformation;
using LightStore.Application.Dtos.Warehouse;
using LightStore.Persistence.Entities.Base;
using System;

namespace LightStore.Application.Dtos.Order
{
    public class OrderVm
    {
        public Guid Id { get; set; }
        public WarehouseMinifiedVm Warehouse { get; set; }
        public DeliveryInformationVm DeliveryInformation { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
