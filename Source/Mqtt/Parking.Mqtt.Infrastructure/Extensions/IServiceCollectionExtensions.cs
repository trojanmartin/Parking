using Microsoft.Extensions.DependencyInjection;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Infrastructure.Mqtt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection service)
        {
            return service.AddSingleton<IMqttService,MqttService>()

                           ;
        }
    }
}
