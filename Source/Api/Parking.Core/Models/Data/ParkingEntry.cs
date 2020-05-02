using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.Data
{
    public class ParkingEntry
    {
        public bool Parked { get; set; }

        public DateTimeOffset TimeStamp { get; set;}
    }
}
