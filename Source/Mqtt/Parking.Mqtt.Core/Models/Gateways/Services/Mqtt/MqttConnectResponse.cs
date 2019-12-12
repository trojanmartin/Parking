using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.Gateways.Services.Mqtt
{
    public class MqttConnectResponse : BaseGatewayResponse
    {       

        public MqttConnectResponse(bool succes = false, IEnumerable<Error> errors = null) : base(succes, errors)
        {
        }
    }
}
