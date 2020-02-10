using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT.Responses;

namespace Parking.Mqtt.Core.Models.MQTT.Requests
{
    public class ConnectRequest : IRequest<ConnectResponse>
    {
        public ConnectRequest(MQTTServerConfigurationDTO configuration)
        {
            ServerConfiguration = configuration;
        }

        public MQTTServerConfigurationDTO ServerConfiguration { get; }
    }
}
