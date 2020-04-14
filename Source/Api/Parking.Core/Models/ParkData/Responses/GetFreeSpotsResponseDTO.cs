using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using System.Collections.Generic;

namespace Parking.Core.Models.ParkData.Responses
{
    public class GetFreeSpotsResponseDTO : BaseResponse
    {
        public IEnumerable<ParkingSpot> ParkingSpots { get; }

        public GetFreeSpotsResponseDTO(IEnumerable<ParkingSpot> parkingSpots, bool success = false, string message = null) : base(success, message)
        {
            ParkingSpots = parkingSpots;
        }
    }
}
