using Parking.Core.Models.Data;
using System.Collections.Generic;

namespace Parking.Core.Models.ParkingLots.Responses
{
    public class GetParkingLotsResponses
    {
        public GetParkingLotsResponses(IEnumerable<ParkingLot> parkingLots)
        {
            ParkingLots = parkingLots;
        }

        public IEnumerable<ParkingLot> ParkingLots { get;}
    }
}
