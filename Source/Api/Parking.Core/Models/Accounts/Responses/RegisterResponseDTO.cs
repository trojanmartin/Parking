using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Core.Models.UseCaseResponses
{
    public class RegisterResponseDTO : BaseResponse
    {

        public ErrorResponse ErrorResponse { get; }

        public RegisterResponseDTO(bool success = false, string message = null) : base(success, message) { }
      

        public RegisterResponseDTO(ErrorResponse errors, bool success = false, string message = null) : base(success, message)
        {
            ErrorResponse = errors;
        }
    }
}
