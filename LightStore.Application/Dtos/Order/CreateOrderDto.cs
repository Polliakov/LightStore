using System;
using System.Collections.Generic;

namespace LightStore.Application.Dtos.ProductInOrder
{
    public class CreateOrderDto
    {
        public Guid WarehouseId { get; set; }
        public CreateDeliveryInformationDto DeliveryInformation { get; set; }
        public List<CreateProductInOrderDto> ProductsInOrder { get; set; }
    }
}
