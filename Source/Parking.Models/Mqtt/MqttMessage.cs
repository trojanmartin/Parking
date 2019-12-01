using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Models.Mqtt
{
    public class MqttMessage
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
        public MqttMessageQoS QoS { get; set; }
    }
}
