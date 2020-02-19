using Parking.Mqtt.Api.Presenters.Base;
using Parking.Mqtt.Api.Serialization;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Net;

namespace Parking.Mqtt.Api.Presenters
{
    public class SubscribePresenter : BasePresenter, IOutputPort<SubscribeResponse>
    {

        public void CreateResponse(SubscribeResponse response)
        {         

            Result.Content = response.Success ? Serializer.SerializeObjectToJson(response) : Serializer.SerializeObjectToJson(response.ErrorResponse);
            Result.StatusCode = (int)((bool)response?.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
