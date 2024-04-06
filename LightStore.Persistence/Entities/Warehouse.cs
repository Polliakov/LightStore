using LightStore.Persistence.Interfaces;
using System;
using System.Collections.Generic;

namespace LightStore.Persistence.Entities
{
    public class Warehouse : ISoftDeletable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime? Deleted { get; set; }

        public List<ProductsAdding> ProductsAddings { get; set; }
        public List<ProductInWarehouse> ProductsInWarehouse { get; set; }
    }
}
