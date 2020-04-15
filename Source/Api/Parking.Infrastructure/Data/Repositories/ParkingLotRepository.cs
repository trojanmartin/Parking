using AutoMapper;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Models.Data;
using Parking.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Infrastructure.Data.Repositories
{
    public class ParkingLotRepository : BaseRepository<Database.Entities.ParkingLot, ParkingLot>, IParkingLotsRepository
    {
        public ParkingLotRepository(ApplicationDbContext mDbContext, IMapper mapper) : base(mDbContext, mapper)
        {
        }

        public Task<IEnumerable<ParkingLot>> GetAllParkingLotsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
