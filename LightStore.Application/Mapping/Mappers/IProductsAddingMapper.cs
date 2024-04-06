using LightStore.Application.Dtos.ProductsAdding;
using LightStore.Persistence.Entities;
using Mapster;
using System;
using System.Linq.Expressions;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface IProductsAddingMapper
    {
        ProductsAddingDetailsVm MapToDetailsDto(ProductsAdding entity);
        Expression<Func<ProductsAdding, ProductsAddingVm>> ProjectToDtos { get; }
        ProductsAdding MapFromDto(CreateProductsAddingDto dto);
    }
}
