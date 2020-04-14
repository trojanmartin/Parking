using System;

namespace Parking.Database.Entities
{
    public class ParkEntry
    {
        public int Id { get; set; }

        public bool Parked { get; set; }

        public DateTimeOffset TimeStamp {get;set;}

        public string SensorId { get; set; }

        public Sensor Sensor { get; set; }
    }
}
