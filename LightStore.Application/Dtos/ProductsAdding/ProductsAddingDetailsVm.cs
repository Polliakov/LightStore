using LightStore.Application.Dtos.Employee;
using LightStore.Application.Dtos.ProductInAdding;
using LightStore.Application.Dtos.Warehouse;
using System;
using System.Collections.Generic;

namespace LightStore.Application.Dtos.ProductsAdding
{
    public class ProductsAddingDetailsVm
    {
        public EmployeeVm Employee { get; set; }
        public WarehouseMinifiedVm Warehouse { get; set; }
        public DateTime CreationDate { get; set; }

        public List<ProductInAddingVm> ProductsInAdding { get; set; }
    }
}
