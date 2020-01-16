using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.UseCaseRequests
{
    public class RegisterRequest : IRequest<RegisterResponse>
    {
        public string Username { get;  }

        public string FirstName { get;  }

        public string  LastName { get;  }

        public string Password { get;  }

        public string Email { get;  }

        public RegisterRequest(string username, string firstName, string lastName, string password, string email)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
        }
    }
}
