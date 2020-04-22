using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Gateways.Repositories
{
    public interface IParkingSpotsRepository : IBaseRepository<ParkingSpot>
    {
        Task<IEnumerable<ParkingSpot>> GetParkingSpots(int parkingLotId);
        Task<IEnumerable<ParkingSpot>> GetParkingSpotWithEntries(int parkingLotId, string spotId);
        Task<ParkingSpot> GetParkingSpotWithLastEntries(int parkingLotId, string spotId);
        Task<IEnumerable<ParkingSpot>> GetParkingSpotWithLastEntries(int parkingLotId);
    }
}
