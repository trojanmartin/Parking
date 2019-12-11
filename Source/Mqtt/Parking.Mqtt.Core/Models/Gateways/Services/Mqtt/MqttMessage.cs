using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.Gateways.Services
{
    public class MqttMessage : BaseGatewayResponse
    {        

        public string Message { get;  }

        public string Topic { get; }

        public string ClientId { get; }

        public MqttMessage(string message, string topic,string clientId ,bool succes = false, IEnumerable<Error> errors = null) : base(succes, errors)
        {
            Message = message;
            Topic = topic;
            ClientId = clientId;
        }
    }
}
