using Parking.Mqtt.Core.Models.Gateways;
using Parking.Mqtt.Core.Models.Gateways.Services;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Services
{
    public interface IMqttService
    {
        event Func<MqttMessage, Task> MessageReceivedAsync;
        Task<MqttListenResponse> BeginListeningAsync(IEnumerable<Topic> topics);
        Task<MqttConnectResponse> ConnectAsync(ConnectRequest connectRequest);
        Task<MqttStatus> GetStatusAsync();
        Task DisconnectAsync();
    }
}
