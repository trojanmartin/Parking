using System.Collections.Generic;

namespace Parking.Database.Entities
{
    public class Sensor
    {
        public int Id { get; set; }

        public int Longitude { get; set; }

        public int Latutide { get; set; }

        public int FCount { get; set; }

        public ICollection<ParkEntry> ParkEntries { get; set; }
    }
}
