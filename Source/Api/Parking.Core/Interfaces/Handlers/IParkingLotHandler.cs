using Parking.Core.Interfaces.Base;
using Parking.Core.Models;
using Parking.Core.Models.ParkingLots.Requests;
using Parking.Core.Models.ParkingLots.Responses;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Handlers
{
    public interface IParkingLotHandler
    {
        Task GetParkingLotByIdAsync(int? parkingLotId, IOutputPort<GetParkingLotsResponseDTO> outputPort);

        Task AddParkingLotAsync(AddParkingLotRequestDTO request, IOutputPort<StandardResponse> outputPort);
    }
}
