using Parking.Core.Interfaces.Base;
using System.Collections.Generic;

namespace Parking.Core.Models.UseCaseResponses
{
    public class LoginResponse : BaseUseCaseResponseMessage
    {
        public User User { get; }

        public Token Token { get; }

        public IEnumerable<Error> Errors { get; }

        public LoginResponse(User user,Token token,bool success = false, string message = null) : base(success, message)
        {
            User = user;
            Token = token;
        }

        public LoginResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }
    }
}
