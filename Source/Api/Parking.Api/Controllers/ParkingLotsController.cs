using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Models.Request.ParkingLots;
using Parking.Api.Routing;
using Parking.Core.Interfaces.Base;
using Parking.Core.Models.Data;
using Parking.Core.Models.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class ParkingLotsController
    {
        /// <summary>
        /// Adds new ParkingLot 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ParkingLotsRouting.Add)]
        [ProducesResponseType(typeof(BaseResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]       
        public Task<IActionResult> AddParkingLotAsync([FromBody] ParkingLotRequest request)
        {
            throw new NotImplementedException();
        }
       

        /// <summary>
        /// Returns ParkingLot of given id. If its null, all ParkingLots are returned
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(ParkingLotsRouting.Get)]
        [ProducesResponseType(typeof(IEnumerable<ParkingLot>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> GetParkingLotsAsync(string parkingLotId)
        {
            throw new NotImplementedException();
        }


    }
}
