using Parking.Mqtt.Api.Serialization;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Presenters
{
    public class ListenPresenter : IOutputPort<ListenResponse>
    {

        public JsonContentResult Result { get; }

        public ListenPresenter()
        {
            Result = new JsonContentResult();
        }

        public void CreateResponse(ListenResponse response)
        {
            Result.Content = Serializer.SerializeObjectToJson(response);
            Result.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
