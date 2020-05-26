using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Gateways.Repositories
{
    public interface IParkingSpotsRepository : IBaseRepository<ParkingSpot>
    {
        Task<IEnumerable<ParkingSpot>> GetParkingSpotsAsync(int parkingLotId);
        Task<IEnumerable<ParkingSpot>> GetParkingSpotWithEntriesAsync(int parkingLotId, string spotId);
        Task<IEnumerable<ParkingSpot>> GetParkingSpotWithEntriesAsync(int parkingLotId, DateTimeOffset? from, DateTimeOffset? to, IEnumerable<string> spotName);
        Task<ParkingSpot> GetParkingSpotWithLastEntryAsync(int parkingLotId, string spotId);
        Task<IEnumerable<ParkingSpot>> GetParkingSpotsWithLastEntriesAsync(int parkingLotId);
    }
}
