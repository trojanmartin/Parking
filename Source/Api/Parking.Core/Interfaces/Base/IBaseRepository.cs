using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Base
{
    public interface IBaseRepository<TEntity>
    {

        Task<TEntity> GetByIdAsync(params object[] Id);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(object Id);

        Task DeleteAsync(TEntity entity);

        Task InsertAsync(TEntity entity);

    }
}
