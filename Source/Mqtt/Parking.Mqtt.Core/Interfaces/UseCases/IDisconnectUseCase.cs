using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.Models.UseCaseResponses;

namespace Parking.Mqtt.Core.Interfaces.UseCases
{
    public interface IDisconnectUseCase : IUseCaseRequestHandler<DisconnectRequest,DisconnectResponse>
    {
    }
}
