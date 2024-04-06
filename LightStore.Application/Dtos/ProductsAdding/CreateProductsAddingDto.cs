using LightStore.Application.Dtos.ProductInAdding;
using System;
using System.Collections.Generic;

namespace LightStore.Application.Dtos.ProductsAdding
{
    public class CreateProductsAddingDto
    {
        public Guid WarehouseId { get; set; }

        public List<ProductInAddingDto> ProductsInAdding { get; set; }
    }
}
