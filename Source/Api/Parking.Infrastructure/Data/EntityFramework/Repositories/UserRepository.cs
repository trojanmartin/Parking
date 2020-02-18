using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Models;
using Parking.Infrastructure.Identity;
using System.Threading.Tasks;

namespace Parking.Infrastructure.Data.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var appUser = _mapper.Map<AppUser>(user);

           return  await _userManager.CheckPasswordAsync(appUser, password);           
        }

        public async Task<bool> CreateUserAsync(User user, string password)
        {

            var appUser = _mapper.Map<AppUser>(user);

            var result = await _userManager.CreateAsync(appUser, password);

            //return new CreateUserResponse(appUser.Id, result.Succeeded, result.Succeeded ? null : result.Errors.Select(e => new Error(e.Code, e.Description)));       

            return result.Succeeded;
        }

        public async Task<User> FindByNameAsync(string username)
        {
            var appUser= await _userManager.FindByNameAsync(username);

            return _mapper.Map<User>(appUser);
        }
    }
}
