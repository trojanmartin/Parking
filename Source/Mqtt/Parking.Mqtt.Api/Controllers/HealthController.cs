using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Mqtt.Api.Routing;
using Parking.Mqtt.Core.Interfaces.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Controllers
{
    
    [Produces("application/json")]
    [ApiController]    
    public class HealthController
    {
        private readonly IHealthHandler _healthHandler;

        public HealthController(IHealthHandler healthHandler)
        {
            _healthHandler = healthHandler;
        }


        [HttpGet]
        [Route(ApiRouting.Health)]
        public async Task<IActionResult> GetHealthAsync()
        {

            //await _healthHandler.GetHealthAsync();

            var res = new Dictionary<string, string>();

            res.Add("Reachable", "True");

            return new OkObjectResult(res);
        }
    }
}
