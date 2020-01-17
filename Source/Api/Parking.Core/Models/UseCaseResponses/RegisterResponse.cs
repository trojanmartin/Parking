using Parking.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.UseCaseResponses
{
    public class RegisterResponse : UseCaseResponseMessage
    {

        public string Id { get; }

        public IEnumerable<Error> Errors { get; }

        public RegisterResponse(string id ,bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }

        public RegisterResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }
    }
}
