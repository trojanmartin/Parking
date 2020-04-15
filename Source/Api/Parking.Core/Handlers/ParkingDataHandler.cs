using Microsoft.Extensions.Logging;
using Parking.Core.Exceptions;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.Handlers;
using Parking.Core.Models;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using Parking.Core.Models.ParkData.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Handlers
{
    public class ParkingDataHandler : IParkingDataHandler
    {
        private readonly IParkingSpotsRepository _spotsRepo;
        private readonly IParkingLotsRepository _lotsRepo;
        private readonly ILogger _logger;

        public ParkingDataHandler(IParkingSpotsRepository spotsRepo, ILogger logger, IParkingLotsRepository lotsRepo)
        {
            _spotsRepo = spotsRepo;
            _logger = logger;
            _lotsRepo = lotsRepo;
        }

        public async Task GetLatestSpotsEntriesAsync(string parkingLotId, string sensorId, IOutputPort<GetCurrentDataReponseDTO> outputPort)
        {  
            try
            {
                var spots = new List<ParkingSpot>();


                if(sensorId == null)
                {
                    spots.AddRange(await _spotsRepo.GetSpotsAsync(parkingLotId));

                    foreach(var spot in spots)
                    {
                        spots.Add(await GetEntriesForSpotAsync(spot, parkingLotId));
                    }

                }
                else
                {
                    var spot = await _spotsRepo.GetByIdAsync(sensorId, parkingLotId);                 
                    spots.Add(await GetEntriesForSpotAsync(spot,parkingLotId));
                }

                outputPort.CreateResponse(new GetCurrentDataReponseDTO(spots, true));
            }
            catch(NotFoundException)
            {
                var meesage = $"ParkingLot with id {parkingLotId} does not exist";

                _logger.LogInformation(meesage);

                outputPort.CreateResponse(new GetCurrentDataReponseDTO(false, new ErrorResponse(new[] { new Error(GlobalErrorCodes.NotFound, meesage) })));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while getting parking entries");

                outputPort.CreateResponse(new GetCurrentDataReponseDTO(false, new ErrorResponse(new[] { GlobalErrors.UnexpectedError })));
            }
        }

        public Task GetSpotsEntriesAsync(string parkingLotId, DateTime from, DateTime to, IEnumerable<string> sensorIds, IOutputPort<GetParkEntriesResponseDTO> outputPort)
        {
            throw new NotImplementedException();
        }

        public Task GetFreeSpotsAsync(string parkingLotId, IOutputPort<GetFreeSpotsResponseDTO> outputPort)
        {
            throw new NotImplementedException();
        }


        private async Task<ParkingSpot> GetEntriesForSpotAsync(ParkingSpot parkingSpot,string parkingLotId)
        {
            var entries = new List<ParkingEntry>();
            entries.Add(await _spotsRepo.GetLastParkingEntryAsync(parkingSpot.Devui, parkingLotId));
            parkingSpot.ParkingEntries = entries;


            return parkingSpot;
        }
    }
}
