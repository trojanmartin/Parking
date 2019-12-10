using Parking.Mqtt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.UseCaseResponses
{
    public class ListenResponse : UseCaseResponseMessage
    {
        public IEnumerable<Error> Errors { get; }


        public ListenResponse(bool succes = false, IEnumerable<Error> errors = null, string message = null ) : base(succes, message)
        {
            Errors = errors;
        }
    }
}
