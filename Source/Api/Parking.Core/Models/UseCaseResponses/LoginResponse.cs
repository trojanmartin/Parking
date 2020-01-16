using Parking.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.UseCaseResponses
{
    public class LoginResponse : UseCaseResponseMessage
    {

        public Token Token { get; }

        public IEnumerable<Error> Errors { get; }

        public LoginResponse(Token token,bool success = false, string message = null) : base(success, message)
        {
            Token = token;
        }

        public LoginResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }
    }
}
