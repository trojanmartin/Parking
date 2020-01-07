using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.UseCases
{
    public class RegisterUserUseCase : IRegisterUseCase
    {
        public Task<bool> HandleAsync(RegisterRequest request, IOutputPort<RegisterResponse> response)
        {
            throw new NotImplementedException();
        }
    }
}
