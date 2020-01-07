using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Interfaces.UseCases
{
    public interface ILoginUseCase : IUseCaseRequestHandler<LoginRequest,LoginResponse>
    {
    }
}
