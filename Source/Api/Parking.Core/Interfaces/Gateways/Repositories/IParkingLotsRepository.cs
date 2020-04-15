using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Gateways.Repositories
{
    public interface IParkingLotsRepository : IBaseRepository<ParkingLot>
    {
        Task<IEnumerable<ParkingLot>> GetAllParkingLotsAsync();
    }
}
