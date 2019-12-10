using System;
using Parking.Mqtt.Library.Models;
using System.Threading.Tasks;
using Parking.Mqtt.Library.Models.Options;

namespace Parking.Mqtt.Library
{
    public interface IMqttProvider
    {
        event Func<MqttMessage, Task> MessageReceived;
        Task<MqttResponse> ConnectAsync(MqttOptions options);
        Task<MqttResponse> PublishMessageAsync(MqttMessage message);
        Task SubscribeAsync(string topic, MqttMessageQoS qoS = MqttMessageQoS.AtLeastOnce);
        Task DisconnectAsync();
    }
}
