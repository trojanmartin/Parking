using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Presenters
{
    public class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";
        }
    }
}
