using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Models.Data;
using Parking.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Infrastructure.Data.Repositories
{
    public class ParkingSpotRepository : BaseRepository<Database.Entities.ParkingSpot, ParkingSpot>, IParkingSpotsRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ParkingSpotRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpots(int parkingLotId)
        {
            return await Task.Run(() =>
           {
               var res = _dbContext.ParkingSpots.Where(x => x.ParkingLotId == parkingLotId);

               return _mapper.Map<IEnumerable<ParkingSpot>>(res);
           });

        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpotWithEntries(int parkingLotId, string spotId)
        {
            return await Task.Run(() =>
            {
                //TODO
                var response = from spot in _dbContext.ParkingSpots
                               join entry in _dbContext.ParkEntries
                               on new { spot.ParkingLotId, spot.Name } equals new { ParkingLotId = entry.ParkingSpotParkingLotId, Name = entry.ParkingSpotName } into spotWithEntries
                               from spotEntry in spotWithEntries
                               where spotEntry.ParkingSpotParkingLotId == parkingLotId && spotEntry.ParkingSpotName == spotId
                               select spotEntry;

                              



                return new[] { new ParkingSpot() };
            });
        }

        public Task<ParkingSpot> GetParkingSpotWithLastEntries(int parkingLotId, string spotId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParkingSpot>> GetParkingSpotWithLastEntries(int parkingLotId)
        {
            throw new NotImplementedException();
        }
    }
}
