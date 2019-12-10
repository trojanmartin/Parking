using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Library.Models
{
    public class MqttResponse
    {
        public MqttResponseCode ResponseCode { get; set; }

        public string Message { get; set; }
    }

    public enum MqttResponseCode
    {
        Success,
        Error
    }

}
