using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Core.Models.UseCaseResponses
{
    public class GetUserResponseDTO : BaseResponse
    {
        public ErrorResponse ErrorResponse { get; }

        public User User { get; }


        public GetUserResponseDTO(User user ,bool success = false, string message = null) : base(success, message)
        {
            User = user;
        }

        public GetUserResponseDTO(ErrorResponse erros, bool success = false, string message = null) : base(success, message)
        {
            ErrorResponse = erros;
        }
    }
}
