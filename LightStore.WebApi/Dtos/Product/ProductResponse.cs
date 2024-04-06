using LightStore.Application.Dtos.Product;

namespace LightStore.WebApi.Dtos.Product
{
    public class ProductResponse : ProductVm
    {
        public string ImageUri { get; set; }
    }
}
