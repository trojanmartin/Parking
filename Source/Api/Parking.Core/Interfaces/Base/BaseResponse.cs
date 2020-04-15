using Parking.Core.Models.Errors;

namespace Parking.Core.Interfaces.Base
{
    public abstract class BaseResponse
    {
        public bool Success { get; }       

        public ErrorResponse ErrorResponse { get; }
        protected BaseResponse(bool success = false, ErrorResponse errorResponse = null)
        {
            Success = success;           
            ErrorResponse = errorResponse;
        }
    }
}
