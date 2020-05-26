using System;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Services
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(object key);

        bool TryGet<T>(object key, out T entry);

        Task SetAsync(object key, object entry);

        T GetOrCreate<T>(object key, Func<T> factory);
    }
}
