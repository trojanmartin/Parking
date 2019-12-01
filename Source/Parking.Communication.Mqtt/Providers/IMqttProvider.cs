using System;
using Parking.Models.Mqtt;
using System.Threading.Tasks;
using Parking.Models.Mqtt.Options;

namespace Parking.Communication.Mqtt
{
    public interface IMqttProvider
    {
        event Func<MqttMessage, Task> MessageReceived;
        Task Connect(MqttOptions options);
        Task PublishMessage(MqttMessage message);
        Task Subscribe(string topic, MqttMessageQoS qoS = MqttMessageQoS.AtLeastOnce);
    }
}
