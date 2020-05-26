using AutoMapper;
using Parking.Database;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;

namespace Parking.Mqtt.Infrastructure.Data.Repositories
{
    public class ParkingSpotRepository : BaseRepository<Database.Entities.ParkingSpot, ParkingSpot>, IParkingSpotRepository
    {
        public ParkingSpotRepository(ApplicationDbContext mDbContext, IMapper mapper) : base(mDbContext, mapper)
        {
        }
    }
}
