using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking.Core.Exceptions;
using Parking.Core.Interfaces.Base;
using System.Threading.Tasks;

namespace Parking.Infrastructure.Data
{
    public abstract class BaseRepository<TDatabaseEntity, TEntity> : IBaseRepository<TEntity>
         where TEntity : class
         where TDatabaseEntity : class
    {
       private readonly DbContext dbContext;
       private readonly IMapper _mapper;
       private readonly DbSet<TDatabaseEntity> dbSet;


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext">The dbContext of the database you want to provide CRUD operations</param>
        protected BaseRepository(DbContext mDbContext, IMapper mapper)
        {
            dbContext = mDbContext;
            dbSet = dbContext.Set<TDatabaseEntity>();
            _mapper = mapper;
        }


        /// <summary>
        /// Provides deleting entities by their Id 
        /// </summary>
        /// <param name="Id">Id of the entity </param>
        public virtual async Task DeleteAsync(object Id)
        {
            var entity = await dbSet.FindAsync(Id);


            await DeleteAsync(_mapper.Map<TEntity>(entity));
        }

        /// <summary>
        /// Provides deleting entities
        /// </summary>
        /// <param name="Id">Entity to delete </param>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            try
            {
                dbSet.Remove(_mapper.Map<TDatabaseEntity>(entity));
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }



        public virtual async Task<TEntity> GetByIdAsync(params object[] Id)
        {
            var res = await dbSet.FindAsync(Id);

            if (res == null)
                throw new NotFoundException();

           return _mapper.Map<TEntity>(res);
        } 

        /// <summary>
        /// Insert entity into database
        /// </summary>
        /// <param name="entity">Entity which should be inserted</param>
        /// <returns></returns>
        public virtual async Task InsertAsync(TEntity entity)
        {
            dbSet.Add(_mapper.Map<TDatabaseEntity>(entity));
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity in database
        /// </summary>
        /// <param name="entity">Entity which should be updated</param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            dbSet.Update(_mapper.Map<TDatabaseEntity>(entity));
            await dbContext.SaveChangesAsync();
        }
    }
}
