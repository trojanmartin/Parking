using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseResponses;

namespace Parking.Core.Models.UseCaseRequests
{
    public class LoginRequestDTO : IRequest<LoginResponseDTO>
    {
        public string Username { get; }
        public string Password { get; }

        public LoginRequestDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
