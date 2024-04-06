using LightStore.Persistence.Entities.Base;
using System;
using System.Collections.Generic;

namespace LightStore.Persistence.Entities
{
    public class Employee : Person
    {
        public Guid AppUserId { get; set; }
        public DateTime? Deleted { get; set; }

        public AppUser AppUser { get; set; }
        public List<ProductsAdding> ProductAddings { get; set; }
    }
}
