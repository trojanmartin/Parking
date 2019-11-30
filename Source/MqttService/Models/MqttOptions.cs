using System;
using System.Collections.Generic;
using System.Text;

namespace MqttService.Models
{
    public class MqttOptions
    {
        public string  ClientId { get; set; }
        public string  TcpServer { get; set; }
        public int  Port { get; set; }
        public string  Username{ get; set; }
        public string  Password{ get; set; }
        public bool UseTls { get; set; }      
    }
}
