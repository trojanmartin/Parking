using Parking.Api.Presenters.Base;
using Parking.Api.Serialization;
using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseResponses;
using System.Net;

namespace Parking.Api.Presenters
{
    public class GetUserPresenter : BasePresenter, IOutputPort<GetUserResponseDTO>
    {
        public void CreateResponse(GetUserResponseDTO response)
        {
            Result.Content = response.Success ?  Serializer.SerializeObjectToJson(response.User) : Serializer.SerializeObjectToJson(response.ErrorResponse);
            Result.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
