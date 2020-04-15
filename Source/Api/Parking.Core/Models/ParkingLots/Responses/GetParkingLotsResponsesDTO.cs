using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Core.Models.ParkingLots.Responses
{
    public class GetParkingLotsResponsesDTO : BaseResponse
    {
        public GetParkingLotsResponsesDTO(IEnumerable<ParkingLot> parkingLots, bool success = false) : base(success)
        {
            ParkingLots = parkingLots;
        }

        public GetParkingLotsResponsesDTO(bool success = false, ErrorResponse errorResponse = null) : base(success, errorResponse)
        {
        }
       
        public IEnumerable<ParkingLot> ParkingLots { get;}
    }
}
