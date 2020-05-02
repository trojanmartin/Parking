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
        private readonly IParkingEntryRepository _entryRepo;
        private readonly IParkingLotsRepository _lotsRepo;
        private readonly ILogger _logger;

        public ParkingDataHandler(IParkingSpotsRepository spotsRepo, ILogger<ParkingDataHandler> logger, IParkingLotsRepository lotsRepo)
        {
            _spotsRepo = spotsRepo;
            _logger = logger;
            _lotsRepo = lotsRepo;
        }

        public async Task GetCurrentSpotsEntriesAsync(int parkingLotId, string spotId, IOutputPort<GetParkingDataResponseDTO> outputPort)
        {
            try
            {

                _logger.LogInformation($"Getting current spot entries for parking lot {parkingLotId} and spotName {spotId}");

                var spots = new List<ParkingSpot>();              
                if (spotId == null)
                {
                    spots.AddRange(await _spotsRepo.GetParkingSpotsWithLastEntriesAsync(parkingLotId));                 

                }

                else
                {
                    spots.Add(await _spotsRepo.GetParkingSpotWithLastEntryAsync(parkingLotId, spotId));
                }

                _logger.LogInformation($"Creating parking data response");
                outputPort.CreateResponse(new GetParkingDataResponseDTO(spots, true));
            }
            catch (NotFoundException)
            {
                var meesage = $"ParkingLot with id {parkingLotId} does not exist";

                _logger.LogInformation(meesage);

                outputPort.CreateResponse(new GetParkingDataResponseDTO(false, new ErrorResponse(new[] { new Error(GlobalErrorCodes.NotFound, meesage) })));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting parking entries");

                outputPort.CreateResponse(new GetParkingDataResponseDTO(false, new ErrorResponse(new[] { GlobalErrors.UnexpectedError })));
            }
        }

        public async Task GetSpotsEntriesAsync(int parkingLotId, DateTime? from, DateTime? to, IEnumerable<string> spotName, IOutputPort<GetParkingDataResponseDTO> outputPort)
        {
            try
            {
                var res = await _spotsRepo.GetParkingSpotWithEntriesAsync(parkingLotId, from, to, spotName);

                outputPort.CreateResponse(new GetParkingDataResponseDTO(res, true));

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while getting parking entries");

                outputPort.CreateResponse(new GetParkingDataResponseDTO(false, new ErrorResponse(new[] { GlobalErrors.UnexpectedError })));
            }

           
        }       
    }
}
