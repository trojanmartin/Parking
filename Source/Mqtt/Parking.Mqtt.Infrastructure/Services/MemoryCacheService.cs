using Microsoft.Extensions.Caching.Memory;
using Parking.Mqtt.Core.Exceptions;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using System;
using System.Threading.Tasks;

namespace Parking.Mqtt.Infrastructure.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly MemoryCache _cache;

        public MemoryCacheService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
               
            });
            
        }

        public async Task SetAsync(object key, object entry)
        {
            await Task.Run(() =>
           {
               var a = _cache.CreateEntry(key);

               _cache.Set(key, entry);
           });
           
        }

        public async Task<T> GetAsync<T>(object key)
        {
            return await Task.Run(() =>
           {
               var entry = _cache.Get<T>(key);

               return entry;//?? throw new NotFoundException($"Object with key {key} is not in memory cache");
           });
        }

        public bool TryGet<T>(object key, out T entry) => _cache.TryGetValue<T>(key, out entry);
       

        public T GetOrCreate<T>(object key, Func<T> factory)
        {
           return _cache.GetOrCreate<T>(key, entry =>
            {
               return factory();
            });
        }
    }
}
