using System;
using System.Collections.Generic;

namespace Parking.Core.Models.Data
{
    public class ParkingSpot
    {        

        public string Name { get;set; }       

        public IEnumerable<ParkingEntry> ParkEntries { get; set;}
    }
}
