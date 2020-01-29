using Parking.Api.Presenters.Base;
using Parking.Api.Serialization;
using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Parking.Api.Presenters
{
    public class GetUserPresenter : BasePresenter, IOutputPort<GetUserResponse>
    {
        public void CreateResponse(GetUserResponse response)
        {
            Result.Content = response.Success ?  Serializer.SerializeObjectToJson(response.User) : Serializer.SerializeObjectToJson(response);
            Result.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
