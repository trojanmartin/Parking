using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.UseCaseResponses
{
    public class GetStatusResponse : UseCaseResponseMessage
    {
        public MqttStatus Status { get; }

        public GetStatusResponse(MqttStatus status , bool success = false, string message = null) : base(success, message)
        {
            Status = status;
        }
    }
}
