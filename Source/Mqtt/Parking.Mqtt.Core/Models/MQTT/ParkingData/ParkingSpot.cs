using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.MQTT.DataMessage
{
    public class ParkingSpot
    {
        public string Name { get; set; }
        public int ParkingLotId { get; set; }

    }
}
