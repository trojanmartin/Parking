using System.Collections.Generic;

namespace Parking.Database.Entities
{
    public class ParkingSpot
    {      
        public string Name { get; set; }
        public int ParkingLotId { get; set; }  

        public ParkingLot ParkingLot { get; set; }  
        
        public ICollection<ParkingEntry>  ParkEntries { get; set; }
        public ICollection<Sensor> Sensors { get; set; }
    }
}
