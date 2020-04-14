namespace Parking.Core.Models.Data
{
    public class ParkingLot
    {
        public ParkingLot(int id, string name, int boxCount, int longitude, int latutide)
        {
            Id = id;
            Name = name;
            BoxCount = boxCount;
            Longitude = longitude;
            Latutide = latutide;
        }

        public int Id { get; }
        public string Name { get; }

        public int BoxCount { get; }

        public int Longitude { get; }

        public int Latutide { get; }
    }
}
