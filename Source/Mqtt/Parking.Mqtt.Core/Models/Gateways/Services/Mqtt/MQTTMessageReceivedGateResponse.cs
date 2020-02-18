using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.Gateways.Services.Mqtt
{
    public class MQTTMessageReceivedGateResponse : BaseGatewayResponse
    {
        public MQTTMessageReceivedGateResponse(MQTTMessageDTO message, bool succes = false) : base(succes)
        {
            Message = message;
        }

        public MQTTMessageReceivedGateResponse(bool succes = false, IEnumerable<Error> errors = null) : base(succes, errors)
        {
        }

        public MQTTMessageDTO Message { get;}
    }
}
