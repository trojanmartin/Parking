using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.MQTT.Responses
{
    public class GetConfigurationResponse : BaseResponse
    {
        public IEnumerable<MQTTServerConfigurationDTO> ServerConfigurations{ get;}       

        public GetConfigurationResponse(ErrorResponse errorResponse, bool success = false, string message = null) : base(success, message, errorResponse)
        {
            
        }

        public GetConfigurationResponse(IEnumerable<MQTTServerConfigurationDTO> serverConfigurations, bool succes) : base(succes)
        {
            ServerConfigurations = serverConfigurations;
        }
    }
}
