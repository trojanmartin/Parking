using Microsoft.Extensions.Logging;
using Parking.Core.Extensions;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.Gateways.Services;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.Models;
using Parking.Core.Models.Errors;
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
        private readonly ILogger _logger;

        public LoginUseCase(ILogger<LoginUseCase> logger,IUserRepository userRepository, IJwtTokenFactory jwtFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _logger = logger;
        }

        public async Task<bool> HandleAsync(LoginRequest request, IOutputPort<LoginResponse> response)
        {
            try
            {
                _logger.LogInformation("Logging user for request {@request}", request);

                if (request.Username.IsValidString() && request.Password.IsValidString())
                {
                    var user = await _userRepository.FindByName(request.Username);

                    if (user != null)
                    {
                        if (await _userRepository.CheckPassword(user, request.Password))
                        {
                            var token = await _jwtFactory.GenerateToken(user.Id, user.UserName);

                            _logger.LogInformation("Login succesfull, token generated succesful");

                            response.CreateResponse(new LoginResponse(user.WithoutPassword(), token, true));
                            return true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                response.CreateResponse(new LoginResponse(new[] { GlobalErrors.UnexpectedError }));
                return false;
            }
   
            response.CreateResponse(new LoginResponse(new[] { new Error("login_failure", "Username or password is invalid") }));
            return false;
        }
    }

}
