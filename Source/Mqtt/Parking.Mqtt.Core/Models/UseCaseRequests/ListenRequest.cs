using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.UseCaseRequests
{
    public class ListenRequest : IRequest<ListenResponse>
    {
        public string ClientId { get; set; }
        public string TcpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseTls { get; set; }
        public bool CleanSession { get; set; }
        public int KeepAlive { get; set; }       
        public List<string> TopicsToSubscribe { get; set; }
    }
}
