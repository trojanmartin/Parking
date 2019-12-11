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

        public IEnumerable<Tuple<string, MqttQualityOfService>> Topics { get; }

        public ListenRequest(IEnumerable<Tuple<string, MqttQualityOfService>> topics)
        {
            Topics = topics;
        }

    }
}
