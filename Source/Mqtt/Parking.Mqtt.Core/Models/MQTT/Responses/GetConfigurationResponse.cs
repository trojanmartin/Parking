using Parking.Mqtt.Core.Interfaces;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.MQTT.Responses
{
    public class GetConfigurationResponse : BaseResponse
    {
        public IEnumerable<MQTTServerConfiguration> ServerConfigurations{ get;}

        public GetConfigurationResponse(bool success = false, IEnumerable<Error> errors = null, string message = null) : base(success, errors, message)
        {
        }

        public GetConfigurationResponse(IEnumerable<MQTTServerConfiguration> serverConfigurations, bool succes) : base(succes)
        {
            ServerConfigurations = serverConfigurations;
        }
    }
}
