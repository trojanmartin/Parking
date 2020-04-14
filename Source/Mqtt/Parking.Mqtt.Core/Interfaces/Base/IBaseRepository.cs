using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Base
{
    public interface IBaseRepository<TEntity>
    {     

        Task<TEntity> GetByIdAsync(object Id);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(int Id);

        Task DeleteAsync(TEntity entity);

        Task InsertAsync(TEntity entity);

    }
}
