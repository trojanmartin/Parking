using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT.Responses;

namespace Parking.Mqtt.Core.Models.MQTT.Requests
{
    public class SaveConfigurationRequest : IRequest<SaveConfigurationResponse>
    {
        public SaveConfigurationRequest(MQTTServerConfigurationDTO mqttServerConfiguration)
        {
            MqttServerConfiguration = mqttServerConfiguration;
        }

        public MQTTServerConfigurationDTO MqttServerConfiguration { get; }
    }
}
