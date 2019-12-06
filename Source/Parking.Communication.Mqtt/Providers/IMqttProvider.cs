using System;
using Parking.Models.Mqtt;
using System.Threading.Tasks;
using Parking.Models.Mqtt.Options;

namespace Parking.Communication.Mqtt.Library
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
