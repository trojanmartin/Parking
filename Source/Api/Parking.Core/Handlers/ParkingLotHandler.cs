using Microsoft.Extensions.Logging;
using Parking.Core.Exceptions;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.Handlers;
using Parking.Core.Models;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using Parking.Core.Models.ParkingLots.Requests;
using Parking.Core.Models.ParkingLots.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Core.Handlers
{
    public class ParkingLotHandler : IParkingLotHandler
    {
        private readonly ILogger _logger;
        private readonly IParkingLotsRepository _lotRepo;

        public ParkingLotHandler(ILogger<ParkingLotHandler> logger, IParkingLotsRepository lotRepo)
        {
            _logger = logger;
            _lotRepo = lotRepo;
        }

        public async Task AddParkingLotAsync(AddParkingLotRequestDTO request, IOutputPort<StandardResultResponseDTO> outputPort)
        {
            try
            {
                _logger.LogInformation("Inserting new ParkingLot. {@ParkingLot}", request.ParkingLot);

                await _lotRepo.InsertAsync(request.ParkingLot);

                outputPort.CreateResponse(new StandardResultResponseDTO(true));

                _logger.LogInformation($"Inserting new parking lot ended sucessfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while inserting new parking lot");

                outputPort.CreateResponse(new StandardResultResponseDTO(false,new ErrorResponse(new[] { GlobalErrors.UnexpectedError })));                
            }

        }

        public async Task GetParkingLotByIdAsync(string parkingLotId, IOutputPort<GetParkingLotsResponsesDTO> outputPort)
        {
            try
            {
                _logger.LogInformation($"Getting parking lot with id {parkingLotId}");

                var parkingLots = new List<ParkingLot>();

                if (parkingLotId == null)
                    parkingLots.Add(await _lotRepo.GetByIdAsync(parkingLotId));

                else
                    parkingLots.AddRange(await _lotRepo.GetAllParkingLotsAsync());


                outputPort.CreateResponse(new GetParkingLotsResponsesDTO(parkingLots, true));

                _logger.LogInformation($"Getting parking lot ended sucessfully");
            }
            catch(NotFoundException)
            {
                _logger.LogInformation($"ParkingLot with id {parkingLotId} does not exist");
                outputPort.CreateResponse(new GetParkingLotsResponsesDTO(false, new ErrorResponse(new[] { new Error(GlobalErrorCodes.NotFound, $"ParkingLot with id {parkingLotId} does not exist") })));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting parking lot");

                outputPort.CreateResponse(new GetParkingLotsResponsesDTO(false, new ErrorResponse(new[] { GlobalErrors.UnexpectedError })));
            }
        }
    }
}
