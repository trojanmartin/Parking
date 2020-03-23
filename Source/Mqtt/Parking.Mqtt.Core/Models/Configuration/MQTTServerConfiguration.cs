using Parking.Mqtt.Core.Models.MQTT.DTO;

namespace Parking.Mqtt.Core.Models.Configuration
{
    public class MQTTServerConfiguration
    {       
        public string ConfigurationName { get; set; }
        public string ClientId { get; set; }
        public string TcpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseTls { get; set; }
        public bool CleanSession { get; set; }
        public int KeepAlive { get; set; }       
    }
}
