using Parking.Core.Models.Data;

namespace Parking.Core.Models.ParkingLots.Requests
{
    public class AddParkingLotRequestDTO
    {
        public AddParkingLotRequestDTO(ParkingLot parkingLot)
        {
            ParkingLot = parkingLot;
        }

        public ParkingLot ParkingLot { get; }

       
    }
}
