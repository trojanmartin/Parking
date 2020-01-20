using Parking.Core.Models;
using Parking.Core.Models.Gateways.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Gateways.Repositories
{
    public interface IUserRepository
    {
        Task<CreateUserResponse> CreateUserAsync(User user, string password);

        Task<User> FindByNameAsync(string username);

        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
