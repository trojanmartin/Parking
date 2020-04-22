using System;

namespace Parking.Database.Entities
{
    public class ParkingEntry
    {
        public int Id { get; set; }
        public bool Parked { get; set; }
        public DateTimeOffset TimeStamp { get; set; }    
        public string ParkingSpotName { get; set; }
        public int ParkingSpotParkingLotId { get; set; }
        public string SensorDevui { get; set; }


        public Sensor Sensor { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
    }
}
