using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Core.Models.UseCaseResponses
{
    public class LoginResponseDTO : BaseResponse
    {       

        public Token Token { get; }       

        public LoginResponseDTO(Token token,bool success = false) : base(success)
        {           
            Token = token;
        }

        public LoginResponseDTO(bool success = false, ErrorResponse errorResponse = null) : base(success, errorResponse)
        {
        }
    }
}
