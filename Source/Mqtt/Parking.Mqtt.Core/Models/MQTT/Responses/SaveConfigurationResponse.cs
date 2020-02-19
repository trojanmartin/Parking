using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.MQTT.Responses
{
    public class SaveConfigurationResponse : BaseResponse
    {      

        public SaveConfigurationResponse(bool success = false, string message = null) : base(success, message)
        {
        }

        public SaveConfigurationResponse(ErrorResponse errorResponse, bool succes = false, string message = null) : base(succes, message,errorResponse)
        {
            
        }
    }
}
