using Parking.Mqtt.Core.Models.MQTT;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.Gateways.Services.Mqtt
{
    public class MQTTGetStatusGateResponse : BaseGatewayResponse
    {
        public bool Connected { get;  }

        public MQTTServerConfigurationDTO ServerConfiguration { get; }

        public MQTTGetStatusGateResponse(bool succes = false, IEnumerable<Error> errors = null) : base(succes, errors)
        {

        }

        public MQTTGetStatusGateResponse(bool connected, MQTTServerConfigurationDTO serverConfiguration, bool succes = false) : base(succes)
        {
            Connected = connected;
            ServerConfiguration = serverConfiguration;
        }
    }
}
