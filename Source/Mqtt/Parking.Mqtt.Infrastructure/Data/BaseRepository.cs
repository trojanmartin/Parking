using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking.Mqtt.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Mqtt.Infrastructure.Data
{
    public abstract class BaseRepository<TDatabaseEntity, TEntity> : IBaseRepository<TEntity> 
        where TEntity : class
        where TDatabaseEntity : class
    {
        protected readonly DbContext dbContext;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TDatabaseEntity> dbSet;


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext">The dbContext of the database you want to provide CRUD operations</param>
        public BaseRepository(DbContext mDbContext, IMapper mapper)
        {
            dbContext = mDbContext;
            dbSet = dbContext.Set<TDatabaseEntity>();
            _mapper = mapper;
        }


        /// <summary>
        /// Provides deleting entities by their Id 
        /// </summary>
        /// <param name="Id">Id of the entity </param>
        public async Task DeleteAsync(params object[] Id)
        {
            var entity = dbSet.Find(Id);


            await DeleteAsync(_mapper.Map<TEntity>(entity));
        }

        /// <summary>
        /// Provides deleting entities
        /// </summary>
        /// <param name="Id">Entity to delete </param>
        public async Task DeleteAsync(TEntity entity)
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



        public async Task<TEntity> GetByIdAsync(params object[] Id) => _mapper.Map<TEntity>(await dbSet.FindAsync(Id));

        /// <summary>
        /// Insert entity into database
        /// </summary>
        /// <param name="entity">Entity which should be inserted</param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            dbSet.Add(_mapper.Map<TDatabaseEntity>(entity));
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity in database
        /// </summary>
        /// <param name="entity">Entity which should be updated</param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            dbSet.Update(_mapper.Map<TDatabaseEntity>(entity));
            await dbContext.SaveChangesAsync();
        }
    }
}
