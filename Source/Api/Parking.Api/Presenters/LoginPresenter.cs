using Parking.Api.Presenters.Base;
using Parking.Api.Serialization;
using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseResponses;
using System.Net;

namespace Parking.Api.Presenters
{
    public class LoginPresenter : BasePresenter, IOutputPort<LoginResponseDTO>
    {
        public void CreateResponse(LoginResponseDTO response)
        {
            Result.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.Unauthorized);
            Result.Content = response.Success ? Serializer.SerializeObjectToJson(response.Token) : Serializer.SerializeObjectToJson(response.ErrorResponse);                
        }
    }
}
