using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.Gateways.Services.Mqtt
{
    public enum MqttQualityOfService
    {
        AtMostOnce = 0,
        AtLeastOnce = 1,
        ExactlyOnce = 2
    }
}
