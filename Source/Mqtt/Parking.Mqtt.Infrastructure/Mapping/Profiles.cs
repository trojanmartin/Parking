using AutoMapper;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;
using Parking.Mqtt.Core.Models.MQTT.ParkingData;

namespace Parking.Mqtt.Infrastructure.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Database.Entities.ParkingEntry, ParkEntry>();
            CreateMap<ParkEntry, Database.Entities.ParkingEntry>();

            CreateMap<Database.Entities.Sensor, Sensor>();
            CreateMap<Sensor, Database.Entities.Sensor>();

            CreateMap<Database.Entities.ParkingSpot, ParkingSpot>();
            CreateMap<ParkingSpot, Database.Entities.ParkingSpot>();
        }
    }
}
