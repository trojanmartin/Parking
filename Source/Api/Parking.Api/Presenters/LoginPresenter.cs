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
    public class LoginPresenter : BasePresenter, IOutputPort<LoginResponse>
    {
        public void CreateResponse(LoginResponse response)
        {
            Result.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.Unauthorized);
            Result.Content = response.Success ? Serializer.SerializeObjectToJson(response) : Serializer.SerializeObjectToJson(response.Errors);                
        }
    }
}
