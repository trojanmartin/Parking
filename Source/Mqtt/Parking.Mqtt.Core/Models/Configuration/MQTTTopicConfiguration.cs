using Parking.Mqtt.Core.Models.Configuration;

namespace Parking.Mqtt.Core.Models.Configuration
{
    public class MQTTTopicConfiguration
    {       

        public string Name { get; set; }

        public MQTTQualityOfService QoS {get; set; }
    }
}
