using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT.Responses;

namespace Parking.Mqtt.Core.Models.MQTT.Requests
{
    public class GetConfigurationRequest : IRequest<GetConfigurationResponse>
    {
        public GetConfigurationRequest(int? id)
        {
            Id = id;
        }

        public int? Id { get; }
    }
}
