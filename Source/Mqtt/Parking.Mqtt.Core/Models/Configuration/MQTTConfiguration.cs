using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.Configuration
{
    public class MQTTConfiguration
    {
        public MQTTServerConfiguration ServerConfiguration { get; set; }

        public MQTTTopicConfiguration[] TopicConfiguration { get; set; }
    }
}
