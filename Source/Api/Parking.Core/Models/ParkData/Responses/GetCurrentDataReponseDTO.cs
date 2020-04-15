using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Core.Models.ParkData.Responses
{
    public class GetCurrentDataReponseDTO : BaseResponse
    {
        public GetCurrentDataReponseDTO(IEnumerable<ParkingSpot> parkingSpots,bool success = false) : base(success)
        {
            ParkingSpots = parkingSpots;
        }

        public GetCurrentDataReponseDTO(bool success = false, ErrorResponse errorResponse = null) : base(success,  errorResponse)
        {
        }

        public IEnumerable<ParkingSpot> ParkingSpots { get; }
    }
}
