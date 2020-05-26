using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Models.Data;
using Parking.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Infrastructure.Data.Repositories
{
    public class ParkingLotRepository : BaseRepository<Database.Entities.ParkingLot, ParkingLot>, IParkingLotsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ParkingLotRepository(ApplicationDbContext mDbContext, IMapper mapper) : base(mDbContext, mapper)
        {
            _context = mDbContext;
            _mapper = mapper;
    }

        public async Task<IEnumerable<ParkingLot>> GetAllParkingLotsAsync()
        {
            var res = await  _context.ParkingLots.ToListAsync();

            return _mapper.Map<IEnumerable<ParkingLot>>(res);
        }
    }
}
