﻿using Parking.Mqtt.Api.Presenters.Base;
using Parking.Mqtt.Api.Serialization;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Net;

namespace Parking.Mqtt.Api.Presenters
{
    public class ConnectPresenter : BasePresenter, IOutputPort<ConnectResponse>
    {
        public void CreateResponse(ConnectResponse response)
        {
            Result.Content = Serializer.SerializeObjectToJson(response);
            Result.StatusCode = (int)((bool)response?.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
