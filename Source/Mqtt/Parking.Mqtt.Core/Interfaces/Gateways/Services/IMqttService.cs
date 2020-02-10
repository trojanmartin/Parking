using Parking.Mqtt.Core.Models.Gateways;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Services
{
    public interface IMqttService
    {
        event Func<MQTTMessageDTO, Task> MessageReceivedAsync;
        Task<MQTTSubscribeGateResponse> SubscribeAsync(IEnumerable<MQTTTopicConfigurationDTO> topics);
        Task<MQTTConnectGateResponse> ConnectAsync(MQTTServerConfigurationDTO configuration);
        Task<MQTTGetStatusGateResponse> GetStatusAsync();
        Task DisconnectAsync();
        Task UnSubscribe(IEnumerable<MQTTTopicConfigurationDTO> topics);
    }
}
