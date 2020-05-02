using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Presenters.Base;
using Parking.Api.Presenters.ParkingData;
using Parking.Api.Routing;
using Parking.Api.Serialization;
using Parking.Core.Interfaces.Handlers;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Parking.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
   // [Authorize]
    public class ParkingDataController
    {

        private readonly IParkingDataHandler _dataHandler;
        private readonly ParkingDataResponsePresenter _parkingDataPresenter;

        public ParkingDataController(IParkingDataHandler dataHandler, ParkingDataResponsePresenter parkingDataPresenter)
        {
            _dataHandler = dataHandler;
            _parkingDataPresenter = parkingDataPresenter;
        }

        /// <summary>
        /// Returns latest data for given ParkingLot. If sensorId is null, data fromm all sensors are returned
        /// </summary>
        /// <param name="parkingLotId">Id of ParkingLot</param>
        /// <param name="sensorId">Deveui of sensor</param>
        /// <returns></returns>
        [HttpGet]
        [Route(ParkingDataRouting.Current)]
        [ProducesResponseType(typeof(IEnumerable<ParkingSpot>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCurrentDataAsync([FromQuery] int parkingLotId, [FromQuery] string sensorId)
        {         
            await _dataHandler.GetCurrentSpotsEntriesAsync(parkingLotId, sensorId, _parkingDataPresenter);

            return _parkingDataPresenter.Result;
        }
       

        /// <summary>
        /// Returns data with specified query. If some parameters are not given, they are ignored 
        /// </summary>
        /// <param name="parkingLotId">Id of ParkingLot, required paramter</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="sensorsIds"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ParkingDataRouting.GetData)]
        [ProducesResponseType(typeof(IEnumerable<ParkingSpot>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDataAsync([FromQuery] int parkingLotId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] IEnumerable<string> spotNames)
        {
            await  _dataHandler.GetSpotsEntriesAsync(parkingLotId, from, to, spotNames, _parkingDataPresenter);

            return _parkingDataPresenter.Result;
        }









    }
}
