using Parking.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.UseCaseResponses
{
    public class RegisterResponse : UseCaseResponseMessage
    {

        public RegisterResponse(bool success = false, string message = null) : base(success, message)
        {
        }
    }
}
