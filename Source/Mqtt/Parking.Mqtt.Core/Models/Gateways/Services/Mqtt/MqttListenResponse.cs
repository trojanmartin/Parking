using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.Gateways
{
    public class MqttListenResponse : BaseGatewayResponse
    {

        public IEnumerable<string> Topics { get; }


        public MqttListenResponse(IEnumerable<string> topics, bool succes = false, IEnumerable<Error> errors = null) : base(succes, errors)
        {
            Topics = topics;
        }
    }
}
