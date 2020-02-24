using System.Collections.Generic;

namespace Parking.Database.Entities
{
    public class ParkingLot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BoxCount { get; set; }

        public ICollection<Sensor> Sensors { get; set; }
    }
}
