using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.Errors;

namespace Parking.Mqtt.Core.Models.MQTT.Responses
{
    public class DisconnectResponse : BaseResponse
    {       

        public DisconnectResponse(ErrorResponse errorResponse, bool success = false, string message = null) : base(success, message, errorResponse)
        {
           
        }

        public DisconnectResponse(bool success = false, string message = null) : base(success, message)
        {
        }
    }
}
