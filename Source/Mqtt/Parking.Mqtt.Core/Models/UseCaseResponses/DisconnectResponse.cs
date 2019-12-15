using Parking.Mqtt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.UseCaseResponses
{
    public class DisconnectResponse : UseCaseResponseMessage
    {
        public DisconnectResponse(bool success = false, string message = null) : base(success, message)
        {
        }
    }
}
