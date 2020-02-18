using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.MQTT.Requests
{
    public class SubscribeRequest : IRequest<SubscribeResponse>
    {
        public SubscribeRequest(IEnumerable<MQTTTopicConfigurationDTO> topics)
        {
            Topics = topics;
        }

        public IEnumerable<MQTTTopicConfigurationDTO> Topics { get; }
    }
}
