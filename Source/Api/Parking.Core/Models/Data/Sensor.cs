using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.Data
{
    public class Sensor
    {
        public string Devui { get; set; }
        public bool Active { get; set; }

        public string ParkingSpotName { get; set; }
        public int ParkingSpotParkingLotId { get; set; }
    }
}
