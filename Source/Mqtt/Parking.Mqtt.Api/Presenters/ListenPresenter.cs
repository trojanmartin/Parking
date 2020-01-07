﻿using Parking.Mqtt.Api.Presenters.Base;
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
    public class ListenPresenter : BasePresenter, IOutputPort<ListenResponse>
    {

        public void CreateResponse(ListenResponse response)
        {
            Result.Content = Serializer.SerializeObjectToJson(response);
            Result.StatusCode = (int)((bool)response?.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
