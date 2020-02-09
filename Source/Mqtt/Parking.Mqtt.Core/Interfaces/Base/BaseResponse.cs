using Parking.Mqtt.Core.Models;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Interfaces
{
    public abstract class BaseResponse
    {
        public bool Success { get; }
        public string Message { get; }

        public IEnumerable<Error> Errors { get; }

        protected BaseResponse(bool success = false, IEnumerable<Error> errors = null, string message = null)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }
    }
}
