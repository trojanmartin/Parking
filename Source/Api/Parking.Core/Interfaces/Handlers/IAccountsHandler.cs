using Parking.Core.Interfaces.Base;
using Parking.Core.Models;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Handlers
{
    public interface IAccountsHandler
    {
        public Task<bool> LogInAsync(LoginRequestDTO loginRequest, IOutputPort<LoginResponseDTO> outputPort);
        public Task<bool> RegisterAsync(RegisterRequestDTO loginRequest, IOutputPort<RegisterResponseDTO> outputPort);
        public Task<bool> GetUserAsync(string name, IOutputPort<GetUserResponseDTO> outputPort);

    }
}
