using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.MQTT.DataMessage
{
    public class SensorData
    {
        public string Name { get; set; }

        public string Devui{ get; set; }

        public string Position { get; set; }

        public IList<ParkEntry> ParkEntries { get; set; }
    }
}
