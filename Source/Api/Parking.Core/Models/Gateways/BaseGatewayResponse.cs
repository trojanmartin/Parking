using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.Gateways
{
    public class BaseGatewayResponse
    {
        public bool Succes { get; }

        public IEnumerable<Error> Errors { get; }

        protected BaseGatewayResponse(bool succes = false, IEnumerable<Error> errors = null)
        {
            Succes = succes;
            Errors = errors;
        }
    }
}
