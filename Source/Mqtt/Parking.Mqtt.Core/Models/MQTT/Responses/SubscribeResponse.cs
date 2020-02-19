using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.Errors;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.MQTT.Responses
{
    public class SubscribeResponse : BaseResponse
    {
        public IEnumerable<MQTTTopicConfigurationDTO> SubscribedTopics { get;}       

        public SubscribeResponse(ErrorResponse errorResponse ,bool success = false,  string message = null) : base(success,  message, errorResponse)
        {
           
        }

        public SubscribeResponse(IEnumerable<MQTTTopicConfigurationDTO> subscribedTopics, bool succes) : base(succes)
        {
            SubscribedTopics = subscribedTopics;
        }
    }
}
