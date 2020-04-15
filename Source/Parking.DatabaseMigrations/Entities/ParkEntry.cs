using System;

namespace Parking.Database.Entities
{
    public class ParkEntry
    {
        public int Id { get; set; }

        public bool Parked { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public string ParkingSpotId { get; set; }

        public ParkingSpot ParkingSpot { get; set; }
    }
}
