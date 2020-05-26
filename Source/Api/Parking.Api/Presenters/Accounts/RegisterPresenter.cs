using Parking.Api.Presenters.Base;
using Parking.Api.Serialization;
using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseResponses;
using System.Net;

namespace Parking.Api.Presenters
{
    public class RegisterPresenter : BasePresenter, IOutputPort<RegisterResponseDTO>
    {
        public void CreateResponse(RegisterResponseDTO response)
        {
            Result.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            Result.Content = response.Success ? Serializer.SerializeObjectToJson((BaseResponse)response) : Serializer.SerializeObjectToJson(response.ErrorResponse);
        }
    }
}
