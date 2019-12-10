using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Library.Models
{

    public enum MqttMessageQoS
    {
        AtMostOnce = 0,
        AtLeastOnce = 1,
        ExactlyOnce = 2
    }

}
