using LightStore.Persistence.Entities.Base;
using LightStore.Persistence.Interfaces;
using System;
using System.Collections.Generic;

namespace LightStore.Persistence.Entities
{
    public class Product : ISoftDeletable
    {
        public Guid Id { get; set; }
        public Guid? ActualPriceId { get; set; }
        public Guid? CategoryId { get; set; }
        public string Title { get; set; }
        public double? Weight { get; set; }
        public string Size { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string Description { get; set; }
        public DateTime? Deleted { get; set; }
        public decimal Price => ActualPrice.Value;

        public ProductPrice ActualPrice { get; set; }
        public List<ProductPrice> PriceHistory { get; set; }
        public ProductsCategory Category { get; set; }
        public List<ProductInCart> ProductInCarts { get; set; }
        public List<ProductInOrder> ProductInOrders { get; set; }
        public List<ProductInAdding> ProductInAddings { get; set; }
        public List<ProductInWarehouse> ProductInWarehouses { get; set; }
    }
}
