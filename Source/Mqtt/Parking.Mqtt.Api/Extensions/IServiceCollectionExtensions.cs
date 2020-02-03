using Microsoft.Extensions.DependencyInjection;
using Parking.Mqtt.Api.Frontend.Presenters;
using Parking.Mqtt.Api.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiModule(this IServiceCollection service)
        {
            return service.AddTransient<ListenPresenter>()
                          .AddTransient<ConnectPresenter>()
                          .AddTransient<DisconnectPresenter>()
                          .AddTransient<GetStatusPresenter>()
                           ;
        }

        public static IServiceCollection AddAdministrationModule(this IServiceCollection service)
        {
            return service.AddTransient<GetStatusWebPresenter>()


                ;
        }
    }
}
