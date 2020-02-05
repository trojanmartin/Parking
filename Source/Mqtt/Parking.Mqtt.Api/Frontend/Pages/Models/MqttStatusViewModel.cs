using Parking.Mqtt.Api.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Frontend.Pages.Models
{
    public class MqttStatusViewModel
    {

        public int ID { get; set; }
        public bool Connected { get; set; }
                public string ClientId { get; set; }
        public string TcpServer { get; set; }
        public int? Port { get; set; }
       // public IEnumerable<Topic> TopicSubscribing { get; set; }
    }
}
