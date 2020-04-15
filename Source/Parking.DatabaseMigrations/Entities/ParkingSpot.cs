namespace Parking.Database.Entities
{
    public class ParkingSpot
    {
        public int Id { get; set; }

        public string ParkingLotId { get; set; }

        public ParkingLot ParkingLot { get; set; }       
    }
}
