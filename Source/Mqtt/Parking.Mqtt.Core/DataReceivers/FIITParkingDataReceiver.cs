using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Parking.Mqtt.Core.Handlers;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models.Configuration;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.DataReceivers
{
    public class FIITParkingDataReceiver : MQTTBackgroundService
    {

        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceFactory;

        public FIITParkingDataReceiver(ILogger<FIITParkingDataReceiver> logger, IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime applicationLifetime, IMqttService client, IOptions<MQTTConfiguration> configs)
                                    : base(logger,client, configs.Value.ServerConfiguration, configs.Value.TopicConfiguration)
        {
            _logger = logger;
            _serviceFactory = serviceScopeFactory;
        }

        public override async Task HandleMessageAsync(MQTTMessageDTO arg)
        {
            await _serviceFactory.CreateScope().ServiceProvider.GetService<IMQTTDataHandler>().ProccesMessage(arg);
        }
    }
}
