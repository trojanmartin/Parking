using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Core.Models.ParkingLots.Responses
{
    public class GetParkingLotsResponseDTO : BaseResponse
    {
        public GetParkingLotsResponseDTO(IEnumerable<ParkingLot> parkingLots, bool success = false) : base(success)
        {
            ParkingLots = parkingLots;
        }

        public GetParkingLotsResponseDTO(bool success = false, ErrorResponse errorResponse = null) : base(success, errorResponse)
        {
        }
       
        public IEnumerable<ParkingLot> ParkingLots { get;}
    }
}
