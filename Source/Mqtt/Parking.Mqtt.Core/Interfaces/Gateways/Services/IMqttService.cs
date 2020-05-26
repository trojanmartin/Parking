using Parking.Mqtt.Core.Models.Configuration;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Services
{
    public interface IMqttService
    {
        event Func<MQTTMessageDTO, Task> MessageReceivedAsync;
        Task SubscribeAsync(IEnumerable<MQTTTopicConfiguration> topics);
        Task ConnectAsync(MQTTServerConfiguration configuration);
        Task<MQTTGetStatusGateResponse> GetStatusAsync();
        Task DisconnectAsync();
        Task Unsubscribe(IEnumerable<MQTTTopicConfiguration> topics);

        void DisposeClient();
    }
}
