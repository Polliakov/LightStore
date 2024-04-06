using System;
using System.Linq.Expressions;
using LightStore.Application.Dtos.Product;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class ProductMapper : IProductMapper
    {
        public Expression<Func<Product, ProductItemVm>> ProjectToItemDtos => p1 => new ProductItemVm()
        {
            Id = p1.Id,
            Title = p1.Title,
            Price = p1.Price,
            UnitOfMeasure = p1.UnitOfMeasure
        };
        public ProductDetailsVm MapToDetailsDto(ValueTuple<Product, int> p2)
        {
            return new ProductDetailsVm()
            {
                CategoryId = p2.Item1.CategoryId,
                Title = p2.Item1.Title,
                AvailabilityCount = p2.Item2,
                Price = p2.Item1.Price,
                Weight = p2.Item1.Weight,
                Size = p2.Item1.Size,
                UnitOfMeasure = p2.Item1.UnitOfMeasure,
                Description = p2.Item1.Description
            };
        }
        public ProductVm MapToDto(ValueTuple<Product, int> p3)
        {
            return new ProductVm()
            {
                AvailabilityCount = p3.Item2,
                Id = p3.Item1.Id,
                Title = p3.Item1.Title,
                Price = p3.Item1.Price,
                UnitOfMeasure = p3.Item1.UnitOfMeasure
            };
        }
        public Product MapFromDto(CreateProductDto p4)
        {
            return p4 == null ? null : new Product()
            {
                CategoryId = p4.CategoryId,
                Title = p4.Title,
                Weight = p4.Weight,
                Size = p4.Size,
                UnitOfMeasure = p4.UnitOfMeasure,
                Description = p4.Description
            };
        }
        public Product MapFromDto(UpdateProductDto p5)
        {
            return p5 == null ? null : new Product()
            {
                Id = p5.Id,
                CategoryId = p5.CategoryId,
                Title = p5.Title,
                Weight = p5.Weight,
                Size = p5.Size,
                UnitOfMeasure = p5.UnitOfMeasure,
                Description = p5.Description
            };
        }
    }
}