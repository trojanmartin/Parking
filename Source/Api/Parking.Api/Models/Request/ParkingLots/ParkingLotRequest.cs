using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Api.Models.Request.ParkingLots
{
    public class ParkingLotRequest
    {
        public string Name { get; set; }

        public int BoxCount { get; set; }

        public int Longitude { get; set; }

        public int Latutide { get; set; }
    }
}
