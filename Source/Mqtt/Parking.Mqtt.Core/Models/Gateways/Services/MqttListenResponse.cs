using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.Gateways
{
    public class MqttListenResponse : BaseGatewayResponse
    {

        public IEnumerable<string> Topics { get; }
        
        public string Url { get; }

        public MqttListenResponse(IEnumerable<string> topics , string url,  bool succes = false, IEnumerable<Error> errors = null) : base(succes, errors)
        {
            Topics = topics;
            Url = url;
        }
    }
}
