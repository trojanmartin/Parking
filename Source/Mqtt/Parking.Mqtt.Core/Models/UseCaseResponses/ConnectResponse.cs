using Parking.Mqtt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.UseCaseResponses
{
    public class ConnectResponse : UseCaseResponseMessage
    {
        IEnumerable<Error> Errors { get; }

        public ConnectResponse(bool succes = false, IEnumerable <Error> errors = null, string message = null) : base(succes, message)
        {
            Errors = errors;
        }
    }
}
