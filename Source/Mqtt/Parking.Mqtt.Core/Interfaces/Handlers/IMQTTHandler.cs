

using Parking.Mqtt.Core.Models.MQTT.Requests;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Handlers
{
    public interface IMQTTHandler
    {
        Task<bool> ConnectAsync(ConnectRequest connectRequest, IOutputPort<ConnectResponse> outputPort);

        Task<bool> SubscribeAsync(SubscribeRequest subscribeRequest, IOutputPort<SubscribeResponse> outputPort);

        Task<bool> DisconnectAsync(DisconnectRequest disconnectRequest, IOutputPort<DisconnectResponse> outputPort);
    }
}
