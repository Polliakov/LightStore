using LightStore.Persistence.Entities.Base;
using System;
using System.Collections.Generic;

namespace LightStore.Persistence.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? DeliveryInformationId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }

        public Warehouse Warehouse { get; set; }
        public Customer Customer { get; set; }
        public DeliveryInformation DeliveryInformation { get; set; }
        public List<ProductInOrder> ProductsInOrder { get; set; }
    }
}
