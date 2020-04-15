using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Errors;

namespace Parking.Core.Models
{
    public class StandardResultResponseDTO : BaseResponse
    {       
        public StandardResultResponseDTO(bool success = false) : base(success)
        {
        }

        public StandardResultResponseDTO(bool success = false, ErrorResponse errorResponse = null) : base(success, errorResponse)
        {
        }
    }
}
