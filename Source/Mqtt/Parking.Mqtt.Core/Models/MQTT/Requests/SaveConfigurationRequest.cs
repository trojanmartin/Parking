using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT.Responses;

namespace Parking.Mqtt.Core.Models.MQTT.Requests
{
    public class SaveConfigurationRequest : IRequest<SaveConfigurationResponse>
    {
        public SaveConfigurationRequest(MQTTServerConfiguration mqttServerConfiguration)
        {
            MqttServerConfiguration = mqttServerConfiguration;
        }

        public MQTTServerConfiguration MqttServerConfiguration { get; }
    }
}
