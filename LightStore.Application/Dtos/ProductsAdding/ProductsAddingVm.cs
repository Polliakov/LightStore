using LightStore.Application.Dtos.Employee;
using LightStore.Application.Dtos.Warehouse;
using System;

namespace LightStore.Application.Dtos.ProductsAdding
{
    public class ProductsAddingVm
    {
        public Guid Id { get; set; }
        public EmployeeVm Employee { get; set; }
        public WarehouseMinifiedVm Warehouse { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
