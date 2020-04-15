using Parking.Core.Interfaces.Base;
using Parking.Core.Models.ParkData.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Handlers
{
    public interface IParkingDataHandler
    {
        Task GetLatestSpotsEntriesAsync(string parkingLotId, string sensorId, IOutputPort<GetCurrentDataReponseDTO> outputPort);

        Task GetFreeSpotsAsync(string parkingLotId, IOutputPort<GetFreeSpotsResponseDTO> outputPort);

        Task GetSpotsEntriesAsync(string parkingLotId, DateTime from, DateTime to, IEnumerable<string> spotIds, IOutputPort<GetParkEntriesResponseDTO> outputPort);
    }
}
