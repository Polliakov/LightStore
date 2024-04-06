using System;

namespace LightStore.Application.Dtos.Warehouse
{
    public class WarehouseVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
    }
}
