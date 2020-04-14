using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.ParkData.Responses
{
    public class GetCurrentDataReponseDTO : BaseResponse
    {
        public GetCurrentDataReponseDTO(IEnumerable<ParkingSpot> parkingSpots,bool success = false, string message = null) : base(success, message)
        {
            ParkingSpots = parkingSpots;
        }

        public IEnumerable<ParkingSpot> ParkingSpots { get; }
    }
}
