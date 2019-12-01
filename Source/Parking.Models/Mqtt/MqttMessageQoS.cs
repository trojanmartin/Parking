using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Models.Mqtt
{

    public enum MqttMessageQoS
    {
        AtMostOnce = 0,
        AtLeastOnce = 1,
        ExactlyOnce = 2
    }

}
