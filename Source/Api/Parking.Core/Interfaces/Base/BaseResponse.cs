﻿using Parking.Core.Models.Errors;
using System.Text.Json.Serialization;

namespace Parking.Core.Interfaces.Base
{
    public abstract class BaseResponse
    {
        public bool Success { get; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; }
        protected BaseResponse(bool success = false, ErrorResponse errorResponse = null)
        {
            Success = success;           
            ErrorResponse = errorResponse;
        }
    }
}
