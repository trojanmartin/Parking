using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;

namespace Parking.Core.Interfaces.UseCases
{
    public interface IGetUserUseCase :  IUseCaseRequestHandler<GetUserRequest,GetUserResponse>
    {
    }
}
