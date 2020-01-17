using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.Gateways.Repositories
{
    public class CreateUserResponse : BaseGatewayResponse
    {

        public string Id { get; }


        public  CreateUserResponse(string id, bool succes = false, IEnumerable<Error> errors = null) : base(succes, errors)
        {
            Id = id;
        }
    }
}
