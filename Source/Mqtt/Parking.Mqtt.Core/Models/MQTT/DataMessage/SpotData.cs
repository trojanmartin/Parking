using System.Collections.Concurrent;

namespace Parking.Mqtt.Core.Models.MQTT.DataMessage
{
    public class SpotData
    {       
        public string SensorDevui{ get; set; }

        public bool Active { get; set; }
        public string Name { get; set; }
        public ConcurrentBag<ParkEntry> ParkEntries { get; set; }
    }
}
