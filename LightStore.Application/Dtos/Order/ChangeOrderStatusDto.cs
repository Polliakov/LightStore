using LightStore.Persistence.Entities.Base;
using System;

namespace LightStore.Application.Dtos.Order
{
    [Obsolete]
    public class ChangeOrderStatusDto
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
