using System.Collections.Generic;

namespace Parking.Database.Entities
{
    public class ParkingLot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BoxCount { get; set; }

        public double Longtitude { get; set; }

        public double Latitude { get; set; }
      
        public ICollection<ParkingSpot> ParkingSpots { get; set; }
    }
}
