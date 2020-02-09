using Microsoft.AspNetCore.Mvc;

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
