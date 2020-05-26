using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Gateways.Repositories
{
    public interface IParkingEntryRepository : IBaseRepository<ParkingEntry>
    {
        Task<IAsyncEnumerable<ParkingEntry>> GetLastParkingEntries(string parkingLotId, string parkingSpot);

        Task<IAsyncEnumerable<ParkingEntry>> GetLastParkingEntries(string parkingLotId);

        Task<IAsyncEnumerable<ParkingEntry>> GetParkingEntriesAsync(object parkingLotId, object spotId);

        Task<IAsyncEnumerable<ParkingEntry>> GetParkingEntriesAsync(object parkingLotId, object spotId, DateTime from, DateTime to);
    }
}
