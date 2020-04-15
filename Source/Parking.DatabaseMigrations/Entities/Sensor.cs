using System.Collections.Generic;

namespace Parking.Database.Entities
{
    public class Sensor
    {  
        public string Devui { get; set; }

        public string Name { get; set; }
       
        public int ParkingSpotId { get; set; }

        public ParkingSpot ParkingSpot { get; set; }
    }
}
