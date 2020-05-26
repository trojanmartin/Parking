using AutoMapper;
using Parking.Database;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Models.MQTT.ParkingData;

namespace Parking.Mqtt.Infrastructure.Data.Repositories
{
    public class SensorRepository : BaseRepository<Database.Entities.Sensor, Sensor>, ISensorRepository
    {
        public SensorRepository(ApplicationDbContext mDbContext, IMapper mapper) : base(mDbContext, mapper)
        {
        }
    }
}
