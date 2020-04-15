using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models.ParkData.Responses
{
    public class GetParkEntriesResponseDTO : BaseResponse
    {
        public IEnumerable<ParkingSpot> ParkingSpots { get; }

        public GetParkEntriesResponseDTO(IEnumerable<ParkingSpot> parkingSpots,bool success = false) : base(success)
        {
            ParkingSpots = parkingSpots;
        }

        public GetParkEntriesResponseDTO(bool success = false, ErrorResponse errorResponse = null) : base(success,  errorResponse)
        {
        }
    }
}
