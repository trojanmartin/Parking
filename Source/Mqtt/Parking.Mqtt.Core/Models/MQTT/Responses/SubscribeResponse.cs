﻿using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.MQTT.Responses
{
    public class SubscribeResponse : BaseResponse
    {
        public IEnumerable<MQTTTopicConfigurationDTO> SubscribedTopics { get;}


        public SubscribeResponse(bool success = false, IEnumerable<Error> errors = null, string message = null) : base(success, errors, message)
        {
        }

        public SubscribeResponse(IEnumerable<MQTTTopicConfigurationDTO> subscribedTopics, bool succes) : base(succes)
        {
            SubscribedTopics = subscribedTopics;
        }
    }
}