using LightStore.Persistence;
using LightStore.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LightStore.Tests.Common
{
    public static class LightStoreDbContextFactory
    {
        public static ILightStoreDbContext Create()
        {
            var options = new DbContextOptionsBuilder<LightStoreDbContext>()
                .UseSqlServer("workstation id=LightStore.mssql.somee.com;packet size=4096;user id=Polliakov_SQLLogin_1;pwd=fxr1gpswmq;data source=LightStore.mssql.somee.com;persist security info=False;initial catalog=LightStore")
                .Options;
            var dbContext = new LightStoreDbContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        public static void Destroy(ILightStoreDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
