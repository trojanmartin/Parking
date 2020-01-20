using Microsoft.Extensions.Logging;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.Models;
using Parking.Core.Models.Errors;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.UseCases
{
    public class RegisterUserUseCase : IRegisterUseCase
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public RegisterUserUseCase(ILogger<RegisterUserUseCase> logger,  IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<bool> HandleAsync(RegisterRequest request, IOutputPort<RegisterResponse> response)
        {
            try
            {
                _logger.LogInformation("Creating user for request {@request}", request);
                var result = await _userRepository.CreateUserAsync(new User(request.FirstName, request.LastName, request.Email, request.Username), request.Password);

                _logger.LogInformation("User repository returned {@response}", result);

                if (result.Succes)
                {
                    response.CreateResponse(new RegisterResponse(result.Id, true, "User succesfully created"));
                    return true;
                }

                else
                {
                    response.CreateResponse(new RegisterResponse(result.Errors, false));
                    return false;
                }
                    
            }

            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                response.CreateResponse(new RegisterResponse(new List<Error> { GlobalErrors.UnexpectedError }));
                return false;
            }
           
        }
    }
}
