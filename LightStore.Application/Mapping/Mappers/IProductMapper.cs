using LightStore.Application.Dtos.Product;
using LightStore.Persistence.Entities;
using Mapster;
using System;
using System.Linq.Expressions;

namespace LightStore.Application.Mapping.Mappers
{
    [Mapper]
    public interface IProductMapper
    {
        ProductDetailsVm MapToDetailsDto((Product product, int availabilityCount) src);
        ProductVm MapToDto((Product product, int availabilityCount) src);
        Expression<Func<Product, ProductItemVm>> ProjectToItemDtos { get; }
        Product MapFromDto(CreateProductDto createProductDto);
        Product MapFromDto(UpdateProductDto updateProductDto);
    }
}
