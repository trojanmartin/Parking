using Parking.Core.Models;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Gateways.Services
{
    public interface IJwtTokenFactory
    {
        Task<Token> GenerateTokenAsync(string id, string username);
    }
}
