using Parking.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.UseCaseResponses
{
    public class GetUserResponse : BaseUseCaseResponseMessage
    {
        public IEnumerable<Error> Errors { get; }

        public User User { get; }


        public GetUserResponse(User user ,bool success = false, string message = null) : base(success, message)
        {
            User = user;
        }

        public GetUserResponse(IEnumerable<Error> erros, bool success = false, string message = null) : base(success, message)
        {
            Errors = erros;
        }
    }
}
