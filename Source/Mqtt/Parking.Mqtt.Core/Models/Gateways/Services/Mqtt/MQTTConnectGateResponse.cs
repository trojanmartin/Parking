using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.Gateways.Services.Mqtt
{
    public class MQTTConnectGateResponse : BaseGatewayResponse
    {      

        public MQTTConnectGateResponse(bool succes = false, IEnumerable<Error> errors = null) : base(succes, errors)
        {
            
        }
    }
}
