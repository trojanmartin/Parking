using System.Collections.Concurrent;

namespace Parking.Mqtt.Core.Models.MQTT.DataMessage
{
    public class SensorData
    {       
        public string Devui{ get; set; }

        public bool Active { get; set; }
        public string Name { get; set; }
        public ConcurrentBag<ParkEntry> ParkEntries { get; set; }
    }
}
