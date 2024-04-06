using LightStore.Application.Dtos.Product;

namespace LightStore.WebApi.Dtos.Product
{
    public class ProductDetailsResponse : ProductDetailsVm
    {
        public string ImageUri { get; set; }
    }
}
