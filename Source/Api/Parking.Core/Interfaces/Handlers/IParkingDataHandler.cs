using Parking.Core.Interfaces.Base;
using Parking.Core.Models.ParkData.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Handlers
{
    public interface IParkingDataHandler
    {
        Task GetCurrentSpotsEntriesAsync(int parkingLotId, string spotId, IOutputPort<GetParkingDataResponseDTO> outputPort);       

        Task GetSpotsEntriesAsync(int parkingLotId, DateTime? from, DateTime? to, IEnumerable<string> spotIds, IOutputPort<GetParkingDataResponseDTO> outputPort);
    }
}
