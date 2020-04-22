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
    [Authorize]
    public class ParkingDataController
    {

        private readonly IParkingDataHandler _dataHandler;
        private readonly ParkingDataResponsePresenter _parkingDataPresenter;

        public ParkingDataController(IParkingDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
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
        public async Task<IActionResult> GetCurrentDataAsync([FromRoute] int parkingLotId, [FromRoute] string sensorId)
        {
            var result = new JsonContentResult()
            {
                Content = Serializer.SerializeObjectToJson(new[]
               {
                    new ParkingSpot()
                    {
                       Name = "TEST",
                       ParkingEntries = new []
                       {
                           new ParkingEntry()
                           {
                               Parked = true,
                               Time = DateTime.Now
                           },
                           new ParkingEntry()
                           {
                               Parked = true,
                               Time = DateTime.Now
                           }
                       }
                    }
                }),
                StatusCode = (int)HttpStatusCode.OK
            };

            return result;


            await _dataHandler.GetLatestSpotsEntriesAsync(parkingLotId, sensorId, _parkingDataPresenter);

            return _parkingDataPresenter.Result;
        }

        /// <summary>
        /// Returned currently free spots from given parking lot
        /// </summary>
        /// <param name="parkingLotId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ParkingDataRouting.Free)]
        [ProducesResponseType(typeof(IEnumerable<ParkingSpot>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFreeSpotsAsync([FromRoute] string parkingLotId)
        {

            var result = new JsonContentResult()
            {
                Content = Serializer.SerializeObjectToJson(new[]
              {
                    new ParkingSpot()
                    {
                       Name = "TEST",
                       ParkingEntries = new []
                       {
                           new ParkingEntry()
                           {
                               Parked = true,
                               Time = DateTime.Now
                           },
                           new ParkingEntry()
                           {
                               Parked = true,
                               Time = DateTime.Now
                           }
                       }
                    }
                }),
                StatusCode = (int)HttpStatusCode.OK
            };

            return result;

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
        [Route(ParkingDataRouting.GetData)]
        [ProducesResponseType(typeof(IEnumerable<ParkingSpot>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDataAsync([FromRoute] string parkingLotId, [FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] IEnumerable<string> sensorsIds)
        {
            var result = new JsonContentResult()
            {
                Content = Serializer.SerializeObjectToJson(new[]
              {
                    new ParkingSpot()
                    {
                       Name = "TEST",
                       ParkingEntries = new []
                       {
                           new ParkingEntry()
                           {
                               Parked = true,
                               Time = DateTime.Now
                           },
                           new ParkingEntry()
                           {
                               Parked = true,
                               Time = DateTime.Now
                           }
                       }
                    }
                }),
                StatusCode = (int)HttpStatusCode.OK
            };

            return result;


            throw new NotImplementedException();
        }









    }
}
