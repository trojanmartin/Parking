using Microsoft.Extensions.Logging;
using Parking.Core.Extensions;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
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
    public class GetUserUseCase : IGetUserUseCase
    {

        private readonly ILogger _logger;
        private readonly IUserRepository _userRepo;

        public GetUserUseCase(ILogger<GetUserUseCase> logger, IUserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        public async Task<bool> HandleAsync(GetUserRequest request, IOutputPort<GetUserResponse> response)
        {

            try
            {
                if (request.Username.IsValidString())
                {
                    var user = await _userRepo.FindByNameAsync(request.Username);

                    if (user != null)
                    {
                        response.CreateResponse(new GetUserResponse(user.WithoutPassword(), true));
                        return true;
                    }                    
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                response.CreateResponse(new GetUserResponse(new[] { GlobalErrors.UnexpectedError }));
                return false;
            }

            response.CreateResponse(new GetUserResponse(new[] { new Error("UserDoesNotExist", $"User with userName {request.Username} does not exist") }, false));
            return false;
        }
    }
}
