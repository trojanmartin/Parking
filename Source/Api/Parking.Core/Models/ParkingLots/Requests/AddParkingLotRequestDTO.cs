﻿using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;

namespace Parking.Core.Models.ParkingLots.Requests
{
    public class AddParkingLotRequestDTO : IRequest<StandardResponse>
    {
        public AddParkingLotRequestDTO(ParkingLot parkingLot)
        {
            ParkingLot = parkingLot;
        }

        public ParkingLot ParkingLot { get; }

       
    }
}
