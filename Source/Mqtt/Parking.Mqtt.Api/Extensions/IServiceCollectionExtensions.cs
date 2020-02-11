using Microsoft.Extensions.DependencyInjection;
using Parking.Mqtt.Api.Frontend.Presenters;
using Parking.Mqtt.Api.Presenters;

namespace Parking.Mqtt.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiModule(this IServiceCollection service)
        {
            return service.AddTransient<SubscribePresenter>()
                          .AddTransient<ConnectPresenter>()
                          .AddTransient<DisconnectPresenter>()                          
                           ;
        }

        public static IServiceCollection AddAdministrationModule(this IServiceCollection service)
        {
            return service.AddSingleton<IndexWebPresenter>()
                          .AddTransient<EditWebPresenter>()

                ;
        }
    }
}
