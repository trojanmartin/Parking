using Microsoft.AspNetCore.Mvc;
using Parking.Mqtt.Api.Routing;
using Parking.Mqtt.Core.Interfaces.Handlers;
using System.Collections.Generic;
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
                     

         

            return new OkObjectResult(await _healthHandler.GetHealthAsync());
        }
    }
}
