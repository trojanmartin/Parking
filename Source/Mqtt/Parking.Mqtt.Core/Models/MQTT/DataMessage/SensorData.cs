using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.MQTT.DataMessage
{
    public class SensorData
    {
        public double Longitude { get; set; }

        public double Latutide { get; set; }

        public int FCount { get; set; }

        public IList<ParkEntry> ParkEntries { get; set; }
    }
}
