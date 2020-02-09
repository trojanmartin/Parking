using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Collections.Generic;

namespace Parking.Mqtt.Api.Frontend.Models
{
    public class MqttStatusViewModel
    {
        public bool Connected { get; }
        public string ClientId { get; }
        public string TcpServer { get; }
        public int? Port { get; }
        public IEnumerable<TopicViewModel> TopicSubscribing { get; }
    }

    public class TopicViewModel
    {
        public string TopicName { get; set; }
        public MQTTQualityOfService QoS { get; set; }
    }
}
