using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.UseCaseRequests
{
    public class ListenRequest : IRequest<ListenResponse>
    {

        public IEnumerable<Topic> Topics { get; }

        public ListenRequest(IEnumerable<Topic> topics)
        {
            Topics = topics;
        }

        
    }

    public class Topic
    {
        public string TopicName { get; set; }
        public MqttQualityOfService QoS { get; set; }
    }

}

