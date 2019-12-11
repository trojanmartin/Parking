using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection service)
        {
            return service;
        }
    }
}
