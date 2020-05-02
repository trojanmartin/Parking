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

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpotsAsync(int parkingLotId)
        {
            return await Task.Run(() =>
           {
               var res = _dbContext.ParkingSpots.Where(x => x.ParkingLotId == parkingLotId);

               return _mapper.Map<IEnumerable<ParkingSpot>>(res);
           });

        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpotWithEntriesAsync(int parkingLotId, string spotId)
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

        public async Task<ParkingSpot> GetParkingSpotWithLastEntryAsync(int parkingLotId, string spotName)
        {

            var all = await GetParkingSpotsWithLastEntriesAsync(parkingLotId);

            return all.FirstOrDefault(x => x.Name == spotName);
        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpotsWithLastEntriesAsync(int parkingLotId)
        {
            return await Task.Run(async () =>
            {

              var lot = await _dbContext.ParkingLots
                                    .Include(x => x.ParkingSpots)
                                        .ThenInclude(spots => spots.ParkEntries)
                                    .SingleOrDefaultAsync(x => x.Id == parkingLotId);

                foreach(var sp in lot.ParkingSpots)
                {
                    sp.ParkEntries = new[] { sp.ParkEntries.OrderBy(x => x.Parked).FirstOrDefault() };
                }


                return _mapper.Map<IEnumerable<ParkingSpot>>(lot.ParkingSpots);               
            });
        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpotWithEntriesAsync(int parkingLotId, DateTimeOffset? from, DateTimeOffset? to, IEnumerable<string> spotNames)
        {
            return await Task.Run(() =>
            {
                var spots = _dbContext.ParkingSpots
                              .Include(x => x.ParkEntries)
                              .Where(x => x.ParkingLotId == parkingLotId);

                if (spotNames.Any())
                {
                    spots = spots.Where(x => spotNames.Contains(x.Name));
                }



                if (from != null)
                {
                    foreach (var sp in spots)
                    {
                        sp.ParkEntries = sp.ParkEntries.Where(x => x.TimeStamp >= from).ToList();
                    }
                }

                if (to != null)
                {
                    foreach (var sp in spots)
                    {
                        sp.ParkEntries = sp.ParkEntries.Where(x => x.TimeStamp <= to).ToList();
                    }
                }


                return _mapper.Map<IEnumerable<ParkingSpot>>(spots);
            });
        }

        
    }
}
