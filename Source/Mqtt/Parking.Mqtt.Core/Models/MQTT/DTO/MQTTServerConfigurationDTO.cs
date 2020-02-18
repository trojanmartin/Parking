using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Collections.Generic;

namespace Parking.Mqtt.Core.Models.MQTT
{
    public class MQTTServerConfigurationDTO
    {



        public MQTTServerConfigurationDTO(string clientId, string tcpServer, int port, string username, string password, bool useTls, bool cleanSession, int keepAlive, int? id = null, IEnumerable<MQTTTopicConfigurationDTO> topics = null, string configurationName = null)
        {
            ClientId = clientId;
            TcpServer = tcpServer;
            Port = port;
            Username = username;
            Password = password;
            UseTls = useTls;
            CleanSession = cleanSession;
            KeepAlive = keepAlive;
            Id = id;
            Topics = topics;
            ConfigurationName = configurationName;           
        }

        public int? Id { get; }
        public string ConfigurationName { get; set; }
        public string ClientId { get; }
        public string TcpServer { get; }
        public int Port { get; }
        public string Username { get; }
        public string Password { get; }
        public bool UseTls { get; }
        public bool CleanSession { get; }
        public int KeepAlive { get; }
        public IEnumerable<MQTTTopicConfigurationDTO> Topics { get; }
    }
}
