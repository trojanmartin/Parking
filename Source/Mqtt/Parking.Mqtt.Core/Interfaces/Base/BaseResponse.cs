using Parking.Mqtt.Core.Models.Errors;
using System.Text.Json.Serialization;

namespace Parking.Mqtt.Core.Interfaces
{
    public abstract class BaseResponse
    {
        public bool Success { get; }
        public string Message { get; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; }

        protected BaseResponse(bool success = false,  string message = null, ErrorResponse errorResponse = null)
        {
            Success = success;
            Message = message;
            ErrorResponse = errorResponse;
        }       
    }
}
