using System;

namespace Parking.Core.Models.Data
{
    public class ParkingSpot
    {
        public ParkingSpot(string name, string devui, string parked, DateTime time)
        {
            Name = name;
            Devui = devui;
            Parked = parked;
            Time = time;
        }

        public string Name { get; }

        public string Devui { get; }

        public string Parked { get; }

        public DateTime Time { get; }
    }
}
