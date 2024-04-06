using System;

namespace LightStore.Persistence.Entities
{
    public class DeliveryInformation
    {
        public Guid Id { get; set; }
        public decimal? Price { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime? Date { get; set; }

        public Order Order { get; set; }
    }
}
