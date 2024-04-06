using LightStore.Persistence.Interfaces;

namespace LightStore.Application.Services.Implementations
{
    public abstract class BaseDataService
    {
        protected BaseDataService(ILightStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected readonly ILightStoreDbContext dbContext;
    }
}
