using Microsoft.Extensions.DependencyInjection;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Infrastructure.Data;
using Parking.Mqtt.Infrastructure.Mqtt;
using Parking.Mqtt.Infrastructure.Services;

namespace Parking.Mqtt.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection service)
        {
            return service.AddSingleton<IMqttService,MqttService>()                         
                          .AddSingleton<ICacheService, MemoryCacheService>()

                          .AddScoped<IParkDataRepository,ParkDataRepository>()
                           ;
        }
    }
}
