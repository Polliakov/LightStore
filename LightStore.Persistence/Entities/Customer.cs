using LightStore.Persistence.Entities.Base;
using System;
using System.Collections.Generic;

namespace LightStore.Persistence.Entities
{
    public class Customer : Person
    {
        public Guid AppUserId { get; set; }
        public Guid CartId { get; set; }
        public DateTime? Deleted { get; set; }

        public AppUser AppUser { get; set; }
        public Cart Cart { get; set; }
        public List<Order> Orders { get; set; }
    }
}
