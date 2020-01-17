using Microsoft.AspNetCore.Mvc;

namespace Parking.Api.Presenters.Base
{
    public class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";
        }
    }
}