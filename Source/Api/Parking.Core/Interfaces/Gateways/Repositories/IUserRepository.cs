using Parking.Core.Models;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Gateways.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User user, string password);

        Task<User> FindByNameAsync(string username);

        Task<bool> CheckPasswordAsync(User user, string password);
       
    }
}
