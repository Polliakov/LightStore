using LightStore.Application.Dtos.Cart;
using LightStore.Application.Dtos.Order;
using LightStore.Application.Dtos.Product;
using LightStore.Application.Dtos.ProductInAdding;
using LightStore.Application.Dtos.ProductInOrder;
using LightStore.Application.Dtos.ProductsAdding;
using LightStore.Application.Dtos.ProductsCategory;
using LightStore.Application.Dtos.Warehouse;
using LightStore.WebApi.Dtos.Cart;
using LightStore.WebApi.Dtos.Order;
using LightStore.WebApi.Dtos.Product;
using LightStore.WebApi.Dtos.ProductInAdding;
using LightStore.WebApi.Dtos.ProductInOrder;
using LightStore.WebApi.Dtos.ProductsAdding;
using LightStore.WebApi.Dtos.ProductsCategory;
using LightStore.WebApi.Dtos.Warehouse;
using Mapster;

namespace LightStore.WebApi.Mapping
{
    public class WebApiMappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(ProductDetailsVm product, string imageUri), ProductDetailsResponse>()
                .Map(dest => dest.ImageUri, src => src.imageUri)
                .Map(dest => dest, src => src.product);
            config.NewConfig<(ProductItemVm product, string imageUri), ProductItemResponse>()
                .Map(dest => dest.ImageUri, src => src.imageUri)
                .Map(dest => dest, src => src.product);
            config.NewConfig<(ProductVm product, string imageUri), ProductResponse>()
                .Map(dest => dest.ImageUri, src => src.imageUri)
                .Map(dest => dest, src => src.product);

            config.NewConfig<(CartItemVm cartItem, string imageUri), CartItemResponse>()
                .Map(dest => dest.ImageUri, src => src.imageUri)
                .Map(dest => dest, src => src.cartItem);

            config.NewConfig<(ProductsCategoryVm category, string imageUri), ProductsCategoryResponse>()
                .Map(dest => dest.ImageUri, src => src.imageUri)
                .Map(dest => dest, src => src.category)
                .Ignore(dest => dest.ChildСategories);

            config.NewConfig<(WarehouseVm warehouse, string imageUri), WarehouseResponse>()
                .Map(dest => dest.ImageUri, src => src.imageUri)
                .Map(dest => dest, src => src.warehouse);

            config.NewConfig<(ProductInAddingVm product, string imageUri), ProductInAddingResponse>()
                .Map(dest => dest.ImageUri, src => src.imageUri)
                .Map(dest => dest, src => src.product);
            config.NewConfig<ProductsAddingDetailsVm, ProductsAddingDetailsResponse>()
                .Ignore(dest => dest.ProductsInAdding);

            config.NewConfig<(ProductInOrderVm product, string imageUri), ProductInOrderResponse>()
                .Map(dest => dest.ImageUri, src => src.imageUri)
                .Map(dest => dest, src => src.product);
            config.NewConfig<CustomersOrderDetailsVm, CustomersOrderDetailsResponse>()
                .Ignore(dest => dest.ProductsInOrder);
            config.NewConfig<OrderDetailsVm, OrderDetailsResponse>()
                .Ignore(dest => dest.ProductsInOrder);
        }
    }
}
