using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Parking.Mqtt.Api.Frontend.Models
{
    public class MqttConfigurationViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        [Display(Name= "Connected ")]
        public bool Connected { get; set; }
        public string ClientId { get; set; }
        public string TcpServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int KeepAlive { get; set; }
        public bool CleanSession { get; set; }
        public bool UseTls { get; set; }
        public int Port { get; set; }
        public IEnumerable<MqttTopicViewModel> TopicSubscribing { get; set; }
    }

    public class MqttTopicViewModel
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
        public MQTTQualityOfService QoS { get; set; }
    }
}
