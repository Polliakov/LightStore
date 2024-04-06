using LightStore.Persistence.Entities;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LightStore.Persistence
{
    public class LightStoreDbContext : DbContext, ILightStoreDbContext
    {
        public LightStoreDbContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductsAdding> ProductsAddings { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryInformation> DeliveryInformation { get; set; }
        public DbSet<ProductsCategory> ProductsCategories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductInCart> ProductsInCarts { get; set; }
        public DbSet<ProductInAdding> ProductsInAddings { get; set; }
        public DbSet<ProductInOrder> ProductsInOrders { get; set; }
        public DbSet<ProductInWarehouse> ProductsInWarehouses { get; set; }

        public void Detach<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Detached;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            builder.ApplySoftDeleteFilters(typeof(ISoftDeletable));
        }
    }
}
