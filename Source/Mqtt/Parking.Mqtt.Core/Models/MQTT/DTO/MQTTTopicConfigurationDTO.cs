namespace Parking.Mqtt.Core.Models.MQTT.DTO
{
    public class MQTTTopicConfigurationDTO
    {
        public MQTTTopicConfigurationDTO(string name, MQTTQualityOfService qoS, int? id = null)
        {
            Name = name;
            QoS = qoS;
            Id = id;
        }

        public int? Id { get; }

        public string Name { get; }

        public MQTTQualityOfService QoS {get;}
    }
}
