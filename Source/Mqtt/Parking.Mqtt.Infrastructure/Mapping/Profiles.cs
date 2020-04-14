using AutoMapper;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Infrastructure.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Database.Entities.ParkEntry, ParkEntry>();
            CreateMap<ParkEntry, Database.Entities.ParkEntry>();

            CreateMap<Database.Entities.Sensor, SensorData>();
            CreateMap<SensorData, Database.Entities.Sensor>();
        }
    }
}
