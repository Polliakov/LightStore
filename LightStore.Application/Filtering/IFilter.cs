using System.Linq;
using System.Threading.Tasks;

namespace LightStore.Application.Filtering
{
    public interface IFilter<TEntity>
    {
        Task<IQueryable<TEntity>> ApplyAsync(IQueryable<TEntity> query);
    }
}
