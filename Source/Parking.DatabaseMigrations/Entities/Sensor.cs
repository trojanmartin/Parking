using System.Collections.Generic;

namespace Parking.Database.Entities
{
    public class Sensor
    {  
        public string Devui { get; set; }             
        public bool Active { get; set; }

        public string ParkingSpotName { get; set; }
        public int ParkingSpotParkingLotId { get; set; }

        public ParkingSpot ParkingSpot { get; set; }

        public ICollection<ParkingEntry> ParkEntries { get; set; }
    }
}
