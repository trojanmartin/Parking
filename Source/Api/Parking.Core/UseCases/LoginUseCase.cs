using Parking.Core.Extensions;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.Gateways.Services;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.Models;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenFactory _jwtFactory;

        public LoginUseCase(IUserRepository userRepository, IJwtTokenFactory jwtFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
        }

        public async Task<bool> HandleAsync(LoginRequest request, IOutputPort<LoginResponse> response)
        {

            if(request.Username.IsValidString() && request.Password.IsValidString())
            {
                var user = await _userRepository.FindByName(request.Username);

                if(user != null)
                {
                    if(await _userRepository.CheckPassword(user, request.Password))
                    {

                        var token = await _jwtFactory.GenerateToken(user.Id, user.UserName);

                        response.CreateResponse(new LoginResponse(token, true));
                        return true;
                    }
                }
            }
            response.CreateResponse(new LoginResponse(new[] { new Error("login_failure", "Username or password is invalid") }));
            return false;
        }
    }

}
