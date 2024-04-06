using LightStore.Application.Dtos.Order;
using LightStore.WebApi.Dtos.ProductInOrder;
using System.Collections.Generic;

namespace LightStore.WebApi.Dtos.Order
{
    public class CustomersOrderDetailsResponse : CustomersOrderDetailsVm
    {
        public new IEnumerable<ProductInOrderResponse> ProductsInOrder { get; set; }
    }
}
