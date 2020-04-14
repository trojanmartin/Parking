using System;

namespace Parking.Mqtt.Core.Models.MQTT.DataMessage
{
    public class ParkEntry
    {
        public bool Parked { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public string SensorId { get; set; }
    }
}
