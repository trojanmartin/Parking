using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Core.Models.UseCaseResponses
{
    public class RegisterResponseDTO : BaseResponse
    {

       

        public RegisterResponseDTO(bool success = false) : base(success) { }

        public RegisterResponseDTO(bool success = false, ErrorResponse errorResponse = null) : base(success, errorResponse)
        {
        }
    }
}
