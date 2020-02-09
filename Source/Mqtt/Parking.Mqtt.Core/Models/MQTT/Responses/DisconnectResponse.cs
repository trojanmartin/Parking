using Parking.Mqtt.Core.Interfaces;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.MQTT.Responses
{
    public class DisconnectResponse : BaseResponse
    {
        public DisconnectResponse(bool success = false, IEnumerable<Error> errors = null, string message = null) : base(success, errors, message)
        {
        }
    }
}
