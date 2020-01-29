using Parking.Core.Interfaces.Base;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.UseCaseRequests
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public string Username { get; }

        public GetUserRequest(string username)
        {
            Username = username;
        }
    }
}
