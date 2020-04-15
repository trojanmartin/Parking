using Parking.Core.Interfaces.Base;
using Parking.Core.Models;
using Parking.Core.Models.ParkingLots.Requests;
using Parking.Core.Models.ParkingLots.Responses;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Handlers
{
    public interface IParkingLotHandler
    {
        Task GetParkingLotByIdAsync(string parkingLotId, IOutputPort<GetParkingLotsResponsesDTO> outputPort);

        Task AddParkingLotAsync(AddParkingLotRequestDTO request, IOutputPort<StandardResultResponseDTO> outputPort);
    }
}
