using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Routing;
using Parking.Core.Models.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ParkingDataController
    {

        /// <summary>
        /// Returns latest data for given ParkingLot. If sensorId is null, data fromm all sensors are returned
        /// </summary>
        /// <param name="parkingLotId">Id of ParkingLot</param>
        /// <param name="sensorId">Deveui of sensor</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route(ParkingDataRouting.Latest)]        
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetCurrentDataAsync([FromRoute] string parkingLotId, [FromRoute] string sensorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returned currently free spots from given parking lot
        /// </summary>
        /// <param name="parkingLotId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route(ParkingDataRouting.Free)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetFreeSpotsAsync([FromRoute] string parkingLotId)
        {
            throw new NotImplementedException();
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route(ParkingDataRouting.GetData)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetParkingDataAsync([FromRoute] string parkingLotId, [FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] IEnumerable<string> sensorsIds)
        {
            throw new NotImplementedException();
        }

        







    }
}
