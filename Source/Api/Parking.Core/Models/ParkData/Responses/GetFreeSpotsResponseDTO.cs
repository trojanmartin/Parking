﻿using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using System.Collections.Generic;

namespace Parking.Core.Models.ParkData.Responses
{
    public class GetFreeSpotsResponseDTO : BaseResponse
    {
        public IEnumerable<ParkingSpot> ParkingSpots { get; }

        public GetFreeSpotsResponseDTO(IEnumerable<ParkingSpot> parkingSpots, bool success = false) : base(success)
        {
            ParkingSpots = parkingSpots;
        }

        public GetFreeSpotsResponseDTO(bool success = false, ErrorResponse errorResponse = null) : base(success,  errorResponse)
        {
        }
    }
}
