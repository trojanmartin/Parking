using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.UseCases
{
    public class LoginUseCaseILoginUseCase
    {
        public Task<bool> HandleAsync(LoginRequest request, IOutputPort<LoginResponse> response)
        {
            throw new NotImplementedException();
        }
    }

}
