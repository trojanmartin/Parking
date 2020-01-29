using Parking.Core.Interfaces.Base;
using System.Collections.Generic;

namespace Parking.Core.Models.UseCaseResponses
{
    public class RegisterResponse : BaseUseCaseResponseMessage
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
