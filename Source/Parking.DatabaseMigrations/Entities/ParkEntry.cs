using System;

namespace Parking.Database.Entities
{
    public class ParkEntry
    {
        public int Id { get; set; }

        public bool Value { get; set; }

        public DateTimeOffset TimeStamp {get;set;}
    }
}
