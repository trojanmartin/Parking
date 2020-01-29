using Parking.Mqtt.Core.Models.UseCaseRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.Gateways.Services.Mqtt
{
    public class MqttStatus
    {
        public bool Connected { get; }
        public string ClientId { get; }
        public string TcpServer { get; }
        public int? Port { get; }
        public IEnumerable<Topic> TopicSubscribing { get; }

        public MqttStatus(bool connected, string clientId, string tcpServer, int? port, IEnumerable<Topic> topicSubscribing)
        {
            Connected = connected;
            ClientId = clientId;
            TcpServer = tcpServer;
            Port = port;
            TopicSubscribing = topicSubscribing;
        }
    }

  
}
