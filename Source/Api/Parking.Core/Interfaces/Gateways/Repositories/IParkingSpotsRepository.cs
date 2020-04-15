using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Gateways.Repositories
{
    public interface IParkingSpotsRepository : IBaseRepository<ParkingSpot>
    {
        Task<IEnumerable<ParkingSpot>> GetSpotsAsync(object parkingLotId);

        Task<IEnumerable<ParkingEntry>> GetParkingEntriesAsync(object parkingLotId,object spotId);

        Task<IEnumerable<ParkingEntry>> GetParkingEntriesAsync(object parkingLotId, object spotId, DateTime from, DateTime to);

        Task<ParkingEntry> GetLastParkingEntryAsync(object parkingLotId,object spotId);

        Task<ParkingEntry> GetLastParkingEntryAsync(object parkingLotId);

        Task<bool> InsertParkEntryAsync(object parkingLotId,IEnumerable<ParkingEntry> parkingEntries);

        Task<bool> InserParkEntryAsync(object parkingLotId,ParkingEntry parkingEntry);
    }
}
