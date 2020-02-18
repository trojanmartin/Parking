using Microsoft.Extensions.Logging;
using Parking.Core.Extensions;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.Gateways.Services;
using Parking.Core.Interfaces.Handlers;
using Parking.Core.Models;
using Parking.Core.Models.Errors;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Handlers
{
    public class AccountsHandler : IAccountsHandler
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenFactory _jwtFactory;
        private readonly ILogger _logger;

        public AccountsHandler(IUserRepository userRepository, IJwtTokenFactory jwtFactory, ILogger<AccountsHandler> logger)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _logger = logger;
        }

        public async Task<bool> GetUserAsync(string name, IOutputPort<GetUserResponseDTO> outputPort)
        {
            try
            {
                if (name.IsValidString())
                {
                    var user = await _userRepository.FindByNameAsync(name);

                    if (user != null)
                    {
                        outputPort.CreateResponse(new GetUserResponseDTO(user.WithoutPassword(), true));
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                outputPort.CreateResponse(new GetUserResponseDTO(new ErrorResponse(new[] { GlobalErrors.UnexpectedError })));
                return false;
            }

            outputPort.CreateResponse(new GetUserResponseDTO(new ErrorResponse(new[] { new Error(GlobalErrorCodes.NotFound, $"User with userName {name} does not exist") }), false));
            return false;
        }

        public async Task<bool> LogInAsync(LoginRequestDTO loginRequest, IOutputPort<LoginResponseDTO> outputPort)
        {
            try
            {
                _logger.LogInformation("Logging user for request {@request}", loginRequest);

                if (loginRequest.Username.IsValidString() && loginRequest.Password.IsValidString())
                {
                    var user = await _userRepository.FindByNameAsync(loginRequest.Username);

                    if (user != null)
                    {
                        if (await _userRepository.CheckPasswordAsync(user, loginRequest.Password))
                        {
                            var token = await _jwtFactory.GenerateTokenAsync(user.Id, user.UserName);

                            _logger.LogInformation("Login succesfull, token generated succesful");

                            outputPort.CreateResponse(new LoginResponseDTO(token, true));
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                outputPort.CreateResponse(new LoginResponseDTO(new ErrorResponse (new[] { GlobalErrors.UnexpectedError })));
                return false;
            }

            outputPort.CreateResponse(new LoginResponseDTO(new ErrorResponse (new[] { new Error(GlobalErrorCodes.InvalidCredentials, "Username or password is invalid") })));
            return false;
        }

        public async Task<bool> RegisterAsync(RegisterRequestDTO loginRequest, IOutputPort<RegisterResponseDTO> outputPort)
        {
            try
            {
                _logger.LogInformation("Creating user for request {@request}", loginRequest);
                var result = await _userRepository.CreateUserAsync(new User(loginRequest.FirstName, loginRequest.LastName, loginRequest.Email, loginRequest.Username), loginRequest.Password);

                _logger.LogInformation("User repository returned {@response}", result);

                if(result)
                    outputPort.CreateResponse(new RegisterResponseDTO(true, "User succesfully created"));

                else
                    outputPort.CreateResponse(new RegisterResponseDTO(new ErrorResponse(new List<Error> {new Error(GlobalErrorCodes.InternalServer,"Cannot create new user") }), false));

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                outputPort.CreateResponse(new RegisterResponseDTO(new ErrorResponse (new List<Error> { GlobalErrors.UnexpectedError })));
                return false;
            }
        }
    }
}
