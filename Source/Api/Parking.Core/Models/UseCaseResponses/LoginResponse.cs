using Parking.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.UseCaseResponses
{
    public class LoginResponse : UseCaseResponseMessage
    {
        public LoginResponse(bool success = false, string message = null) : base(success, message)
        {
        }
    }
}
