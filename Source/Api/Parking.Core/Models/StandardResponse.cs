using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Errors;

namespace Parking.Core.Models
{
    public class StandardResponse : BaseResponse
    {       
        public string Message { get; }

        public StandardResponse(bool success = false, string meesage = null) : base(success)
        {
            Message = meesage;
        }

        public StandardResponse(bool success = false, ErrorResponse errorResponse = null) : base(success, errorResponse)
        {
        }
    }
}
