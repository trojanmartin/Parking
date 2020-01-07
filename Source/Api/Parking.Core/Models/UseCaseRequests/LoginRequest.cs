using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.UseCaseRequests
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Username { get; }
        public string Password { get; }

        public LoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
