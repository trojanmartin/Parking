using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.Gateways
{
    public class MQTTSubscribeGateResponse : BaseGatewayResponse
    {

        public IEnumerable<MQTTTopicConfigurationDTO> Topics { get; }


        public MQTTSubscribeGateResponse(IEnumerable<MQTTTopicConfigurationDTO> topics = null, bool succes = false, IEnumerable<Error> errors = null) : base(succes, errors)
        {
            Topics = topics;
        }       
    }
}
