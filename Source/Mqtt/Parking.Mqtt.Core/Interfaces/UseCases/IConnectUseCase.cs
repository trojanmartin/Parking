using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Interfaces.UseCases
{
    public interface IConnectUseCase : IUseCaseRequestHandler<ConnectRequest,ConnectResponse>
    {
    }
}
