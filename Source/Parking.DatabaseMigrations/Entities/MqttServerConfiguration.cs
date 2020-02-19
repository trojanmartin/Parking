using System.Collections.Generic;

namespace Parking.Database.Entities
{
    public class MqttServerConfiguration
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TCPServer { get; set; }

        public int  Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool UseTls { get; set; }

        public bool CleanSession { get; set; }

        public int KeepAlive { get; set; }

        public ICollection<MqttTopicConfiguration> Topics { get; set; }
    }
}
