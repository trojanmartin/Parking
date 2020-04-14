using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking.Database;
using Parking.Database.Entities;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;

namespace Parking.Mqtt.Infrastructure.Data.Repositories
{
    public class SensorRepository : BaseRepository<Sensor, SensorData>, ISensorRepository
    {
        public SensorRepository(ApplicationDbContext mDbContext, IMapper mapper) : base(mDbContext, mapper)
        {
        }
    }
}
