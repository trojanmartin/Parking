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

        public float Longtitude { get; set; }

        public float Latitude { get; set; }
    }
}
