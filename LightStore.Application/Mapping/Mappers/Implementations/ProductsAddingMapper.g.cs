using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LightStore.Application.Dtos.Employee;
using LightStore.Application.Dtos.ProductInAdding;
using LightStore.Application.Dtos.ProductsAdding;
using LightStore.Application.Dtos.Warehouse;
using LightStore.Application.Mapping.Mappers;
using LightStore.Persistence.Entities;

namespace LightStore.Application.Mapping.Mappers.Implementations
{
    public partial class ProductsAddingMapper : IProductsAddingMapper
    {
        public Expression<Func<ProductsAdding, ProductsAddingVm>> ProjectToDtos => p1 => new ProductsAddingVm()
        {
            Id = p1.Id,
            Employee = p1.Employee == null ? null : new EmployeeVm()
            {
                Surname = p1.Employee.Surname,
                Name = p1.Employee.Name,
                Patronymic = p1.Employee.Patronymic
            },
            Warehouse = p1.Warehouse == null ? null : new WarehouseMinifiedVm()
            {
                Id = p1.Warehouse.Id,
                Name = p1.Warehouse.Name
            },
            CreationDate = p1.CreationDate
        };
        public ProductsAddingDetailsVm MapToDetailsDto(ProductsAdding p2)
        {
            return p2 == null ? null : new ProductsAddingDetailsVm()
            {
                Employee = p2.Employee == null ? null : new EmployeeVm()
                {
                    Surname = p2.Employee.Surname,
                    Name = p2.Employee.Name,
                    Patronymic = p2.Employee.Patronymic
                },
                Warehouse = p2.Warehouse == null ? null : new WarehouseMinifiedVm()
                {
                    Id = p2.Warehouse.Id,
                    Name = p2.Warehouse.Name
                },
                CreationDate = p2.CreationDate,
                ProductsInAdding = funcMain1(p2.ProductsInAdding)
            };
        }
        public ProductsAdding MapFromDto(CreateProductsAddingDto p4)
        {
            return p4 == null ? null : new ProductsAdding()
            {
                WarehouseId = p4.WarehouseId,
                ProductsInAdding = funcMain2(p4.ProductsInAdding)
            };
        }
        
        private List<ProductInAddingVm> funcMain1(List<ProductInAdding> p3)
        {
            if (p3 == null)
            {
                return null;
            }
            List<ProductInAddingVm> result = new List<ProductInAddingVm>(p3.Count);
            
            int i = 0;
            int len = p3.Count;
            
            while (i < len)
            {
                ProductInAdding item = p3[i];
                result.Add(item == null ? null : new ProductInAddingVm()
                {
                    ProductId = item.ProductId,
                    Title = item.Product == null ? null : item.Product.Title,
                    Count = item.Count
                });
                i++;
            }
            return result;
            
        }
        
        private List<ProductInAdding> funcMain2(List<ProductInAddingDto> p5)
        {
            if (p5 == null)
            {
                return null;
            }
            List<ProductInAdding> result = new List<ProductInAdding>(p5.Count);
            
            int i = 0;
            int len = p5.Count;
            
            while (i < len)
            {
                ProductInAddingDto item = p5[i];
                result.Add(item == null ? null : new ProductInAdding()
                {
                    ProductId = item.ProductId,
                    Count = item.Count
                });
                i++;
            }
            return result;
            
        }
    }
}