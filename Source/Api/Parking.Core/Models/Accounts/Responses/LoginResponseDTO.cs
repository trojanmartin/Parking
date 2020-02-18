using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Core.Models.UseCaseResponses
{
    public class LoginResponseDTO : BaseResponse
    {       

        public Token Token { get; }

        public ErrorResponse ErrorResponse { get; }

        public LoginResponseDTO(Token token,bool success = false, string message = null) : base(success, message)
        {           
            Token = token;
        }

        public LoginResponseDTO(ErrorResponse errors, bool success = false, string message = null) : base(success, message)
        {
            ErrorResponse = errors;
        }
    }
}
