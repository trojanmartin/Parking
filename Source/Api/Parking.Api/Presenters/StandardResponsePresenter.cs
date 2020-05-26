using Parking.Api.Presenters.Base;
using Parking.Api.Serialization;
using Parking.Core.Interfaces.Base;
using Parking.Core.Models;
using System.Net;

namespace Parking.Api.Presenters
{
    public class StandardResponsePresenter : BasePresenter, IOutputPort<StandardResponse>
    {
        public void CreateResponse(StandardResponse response)
        {
            Result.Content = response.Success ? Serializer.SerializeObjectToJson(response) : Serializer.SerializeObjectToJson(response.ErrorResponse);
            Result.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
        }
    }    
}
