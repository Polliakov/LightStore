using System.Collections.Generic;

namespace LightStore.Application.Dtos.Order
{
    public class OrdersPageVm
    {
        public int FoundCount { get; set; }
        public List<OrderVm> Orders { get; set; }
    }
}
