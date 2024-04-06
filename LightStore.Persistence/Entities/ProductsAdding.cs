using System;
using System.Collections.Generic;

namespace LightStore.Persistence.Entities
{
    public class ProductsAdding
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid WarehouseId { get; set; }
        public DateTime CreationDate { get; set; }

        public Employee Employee { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<ProductInAdding> ProductsInAdding { get; set; }
    }
}
