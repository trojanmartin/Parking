using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Collections.Generic;

namespace Parking.Mqtt.Api.Models.Requests
{
    public class ListenApiRequest
    {
       public IEnumerable<Topic> Topics { get; set; } 
    }

    public class Topic
    {
        public string TopicName { get; set; }
        public MQTTQualityOfService QoS { get; set; }
    }
}
