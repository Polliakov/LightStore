using LightStore.Application.Dtos.ProductInWarehouse;
using System;

namespace LightStore.Application.Exceptions
{
    public class WarehouseWriteoffException : Exception
    {
        public WarehouseWriteoffException(Guid warehouseId, int currentCount, IChangeProductCountDto dto)
            : base($"Cant't writeoff product ({dto.ProductId}) in warehouse ({warehouseId}). " +
                   $"Current count ({currentCount}), try writeoff ({dto.Count}).")
        { }
    }
}
