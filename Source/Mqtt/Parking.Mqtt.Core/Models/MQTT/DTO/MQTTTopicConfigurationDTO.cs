namespace Parking.Mqtt.Core.Models.MQTT.DTO
{
    public class MQTTTopicConfigurationDTO
    {
        public MQTTTopicConfigurationDTO(string name, MQTTQualityOfService qoS)
        {
            Name = name;
            QoS = qoS;
        }

        public string Name { get; }

        public MQTTQualityOfService QoS {get;}
    }
}
