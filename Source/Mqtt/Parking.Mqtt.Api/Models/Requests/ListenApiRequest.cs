using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Models.Requests
{
    public class ListenApiRequest
    {
       public IEnumerable<Topic> Topics { get; set; } 
    }

    public class Topic
    {
        public string TopicName { get; set; }
        public MqttQualityOfService QoS { get; set; }
    }
}
