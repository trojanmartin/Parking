using AutoMapper;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Models.Data;
using Parking.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Infrastructure.Data.Repositories
{
    public class ParkingSpotRepository : BaseRepository<Database.Entities.Sensor,ParkingSpot>, IParkingSpotsRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ParkingSpotRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext,mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<ParkingEntry> GetLastParkingEntryAsync(object parkingLotId, object spotId)
        {

            throw new NotImplementedException();
        }

        public Task<ParkingEntry> GetLastParkingEntryAsync(object parkingLotId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParkingEntry>> GetParkingEntriesAsync(object parkingLotId, object spotId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParkingEntry>> GetParkingEntriesAsync(object parkingLotId, object spotId, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParkingSpot>> GetSpotsAsync(object parkingLotId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InserParkEntryAsync(object parkingLotId, ParkingEntry parkingEntry)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertParkEntryAsync(object parkingLotId, IEnumerable<ParkingEntry> parkingEntries)
        {
            throw new NotImplementedException();
        }
    }
}
