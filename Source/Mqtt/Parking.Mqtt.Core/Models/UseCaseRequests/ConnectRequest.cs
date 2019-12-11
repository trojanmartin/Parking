using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.UseCaseRequests
{
    public class ConnectRequest : IRequest<ConnectResponse>
    {
        public string ClientId { get;  }
        public string TcpServer { get;  }
        public int Port { get;  }
        public string Username { get;  }
        public string Password { get;  }
        public bool UseTls { get;  }
        public bool CleanSession { get;  }
        public int KeepAlive { get;  }

        public ConnectRequest(string clientId, string tcpServer, int port, string username, string password, bool useTls, bool cleanSession, int keepAlive)
        {
            ClientId = clientId;
            TcpServer = tcpServer;
            Port = port;
            Username = username;
            Password = password;
            UseTls = useTls;
            CleanSession = cleanSession;
            KeepAlive = keepAlive;
        }
    }
}
