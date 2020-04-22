namespace Parking.Mqtt.Core.Models.MQTT.ParkingData
{
    public class Sensor
    {
        public string Devui { get; set; }
        public bool Active { get; set; }

        public string ParkingSpotName { get; set; }
        public int ParkingSpotParkingLotId { get; set; }
    }
}
