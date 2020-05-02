using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Models.Request.ParkingLots;
using Parking.Api.Presenters;
using Parking.Api.Presenters.Base;
using Parking.Api.Presenters.ParkingLots;
using Parking.Api.Routing;
using Parking.Api.Serialization;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Handlers;
using Parking.Core.Models;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using Parking.Core.Models.ParkingLots.Requests;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Parking.Api.Controllers
{

    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ParkingLotsController
    {
        private readonly IParkingLotHandler _lotHandler;
        private readonly StandardResponsePresenter _standardPresenter;
        private readonly GetParkingLotsPresenter _getParkingLotsPresenter;

        public ParkingLotsController(IParkingLotHandler lotHandler, StandardResponsePresenter standardPresenter, GetParkingLotsPresenter getParkingLotsPresenter)
        {
            _lotHandler = lotHandler;
            _standardPresenter = standardPresenter;
            _getParkingLotsPresenter = getParkingLotsPresenter;
        }


        /// <summary>
        /// Adds new ParkingLot 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ParkingLotsRouting.Add)]
        [ProducesResponseType(typeof(StandardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddParkingLotAsync([FromBody] ParkingLotRequest request)
        {
            

            await _lotHandler.AddParkingLotAsync(new AddParkingLotRequestDTO(new ParkingLot()
            {
                Name = request.Name,
                BoxCount = request.BoxCount,
                Latitude = request.Latitude,
                Longtitude = request.Longtitude
            }), _standardPresenter);


            return _standardPresenter.Result;
        }


        /// <summary>
        /// Returns ParkingLot of given id. If its null, all ParkingLots are returned
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(ParkingLotsRouting.Get)]
        [ProducesResponseType(typeof(IEnumerable<ParkingLot>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetParkingLotsAsync([FromQuery] int? parkingLotId)
        {
          
            await _lotHandler.GetParkingLotByIdAsync(parkingLotId, _getParkingLotsPresenter);

            return _getParkingLotsPresenter.Result;
        }


    }
}
