using System;
using System.Collections.Generic;
using System.Text;

namespace MqttService.Models
{
    public class MqttMessage
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
        public MqttMessageQoS QoS { get; set; }
    }

    public enum MqttMessageQoS
    {
        AtMostOnce = 0,
        AtLeastOnce = 1,
        ExactlyOnce = 2
    }
}
