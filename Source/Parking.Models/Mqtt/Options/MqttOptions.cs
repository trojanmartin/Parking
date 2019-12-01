using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Models.Mqtt.Options
{
    public class MqttOptions
    {
        public string ClientId { get; set; }
        public string TcpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseTls { get; set; }
        public bool CleanSession { get; set; }
        public int KeepAlive { get; set; }
        public ReconnectOptions ReconnectOptions { get; set; }
        public List<string> TopicsToSubscribe { get; set; }
    }
}
