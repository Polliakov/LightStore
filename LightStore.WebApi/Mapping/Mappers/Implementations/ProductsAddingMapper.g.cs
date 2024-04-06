using LightStore.Application.Dtos.Employee;
using LightStore.Application.Dtos.ProductsAdding;
using LightStore.Application.Dtos.Warehouse;
using LightStore.WebApi.Dtos.ProductsAdding;
using LightStore.WebApi.Mapping.Mappers;

namespace LightStore.WebApi.Mapping.Mappers.Implementations
{
    public partial class ProductsAddingMapper : IProductsAddingMapper
    {
        public ProductsAddingDetailsResponse MapToDetailsResponse(ProductsAddingDetailsVm p1)
        {
            return p1 == null ? null : new ProductsAddingDetailsResponse()
            {
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
        }
    }
}