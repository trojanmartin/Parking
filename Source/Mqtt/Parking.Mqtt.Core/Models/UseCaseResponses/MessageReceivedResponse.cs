using Parking.Mqtt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.UseCaseResponses
{
    public class MessageReceivedResponse : UseCaseResponseMessage
    {
        public string Payload { get; }

        public string Topic { get; }


        public MessageReceivedResponse(string payload, string topic)
        {
            Payload = payload;
            Topic = topic;
        }
    }
}
