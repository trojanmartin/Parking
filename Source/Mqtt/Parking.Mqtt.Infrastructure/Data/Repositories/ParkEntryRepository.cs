using AutoMapper;
using Parking.Database;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Infrastructure.Data.Repositories
{
    public class ParkEntryRepository : BaseRepository<Database.Entities.ParkEntry, ParkEntry>, IParkEntryRepository
    {       

        public ParkEntryRepository(ApplicationDbContext _context, IMapper mapper) : base(_context,mapper)
        {
           
        }      
    }
}
