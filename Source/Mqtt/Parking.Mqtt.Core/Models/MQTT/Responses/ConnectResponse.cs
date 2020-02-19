using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.Errors;

namespace Parking.Mqtt.Core.Models.MQTT.Responses
{
    public class ConnectResponse : BaseResponse
    {        
        public MQTTServerConfigurationDTO ConnectedConfiguration { get; }

        public ConnectResponse(ErrorResponse errorResponse,bool success = false,string message = null) : base(success, message,errorResponse)
        {
            
        }

        public ConnectResponse(MQTTServerConfigurationDTO connectedConfiguration, bool succes) : base(succes)
        {
            ConnectedConfiguration = connectedConfiguration;
        }
    }
}
