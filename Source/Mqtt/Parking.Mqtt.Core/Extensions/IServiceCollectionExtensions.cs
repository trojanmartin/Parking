using Microsoft.Extensions.DependencyInjection;
using Parking.Mqtt.Core.Handlers;
using Parking.Mqtt.Core.Interfaces.Handlers;

namespace Parking.Mqtt.Core.Extensions
{
    public static class IServiceCollectionExtensions
    { 

        public static IServiceCollection AddCoreModule(this IServiceCollection service)
        {
            return service.AddTransient<IMQTTHandler, MQTTHandler>()
                          .AddTransient<IDataReceivedHandler, DataReceivedHandler>()                       
                           
                           ;
        }
    }
}
