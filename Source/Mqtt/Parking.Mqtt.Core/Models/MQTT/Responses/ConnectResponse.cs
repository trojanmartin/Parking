using Parking.Mqtt.Core.Interfaces;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.MQTT.Responses
{
    public class ConnectResponse : BaseResponse
    {        
        public MQTTServerConfigurationDTO ConnectedConfiguration { get; }

        public ConnectResponse(bool success = false, IEnumerable<Error> errors = null, string message = null) : base(success, errors, message)
        {
        }

        public ConnectResponse(MQTTServerConfigurationDTO connectedConfiguration, bool succes) : base(succes)
        {
            ConnectedConfiguration = connectedConfiguration;
        }
    }
}
