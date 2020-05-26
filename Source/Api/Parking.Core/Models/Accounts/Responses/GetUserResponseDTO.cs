using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Errors;

namespace Parking.Core.Models.UseCaseResponses
{
    public class GetUserResponseDTO : BaseResponse
    {     

        public User User { get; }


        public GetUserResponseDTO(User user ,bool success = false) : base(success)
        {
            User = user;
        }

        public GetUserResponseDTO(bool success = false, ErrorResponse errorResponse = null) : base(success, errorResponse)
        {
        }
    }
}
