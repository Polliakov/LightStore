using LightStore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LightStore.Persistence.Interfaces
{
    public interface ILightStoreDbContext : IDisposable
    {
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductPrice> ProductPrices { get; set; }
        DbSet<ProductsAdding> ProductsAddings { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<DeliveryInformation> DeliveryInformation { get; set; }
        DbSet<ProductsCategory> ProductsCategories { get; set; }
        DbSet<Warehouse> Warehouses { get; set; }
        DbSet<ProductInCart> ProductsInCarts { get; set; }
        DbSet<ProductInAdding> ProductsInAddings { get; set; }
        DbSet<ProductInOrder> ProductsInOrders { get; set; }
        DbSet<ProductInWarehouse> ProductsInWarehouses { get; set; }

        DatabaseFacade Database { get; }

        void Detach<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
