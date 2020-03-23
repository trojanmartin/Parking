using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parking.Mqtt.Api.Presenters;
using Parking.Mqtt.Core.Models.Configuration;

namespace Parking.Mqtt.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiModule(this IServiceCollection service,IConfiguration configuration)
        {
            return service
                          .AddOptions()
                          .Configure<MQTTConfiguration>(configuration.GetSection("MQTTConfiguration"))



                ;
        }


        
    }
}
