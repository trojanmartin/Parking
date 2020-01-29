using Microsoft.Extensions.DependencyInjection;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.UseCases;

namespace Parking.Mqtt.Core.Extensions
{
    public static class IServiceCollectionExtensions
    { 

        public static IServiceCollection AddCoreModule(this IServiceCollection service)
        {
            return service.AddTransient<IConnectUseCase, MqttConnectUseCase>()
                           .AddTransient<IListenUseCase, MqttListenUseCase>()
                           .AddTransient<IDisconnectUseCase,MqttDisconnectUseCase>()
                           .AddTransient<IGetStatusUseCase, MqttGetStatusUseCase>()
                           ;
        }
    }
}
