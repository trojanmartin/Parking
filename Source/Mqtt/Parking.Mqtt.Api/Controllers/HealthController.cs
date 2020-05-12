using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Mqtt.Api.Routing;
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
       

        public HealthController()
        {
           
        }


        [HttpGet]
        [Route(ApiRouting.Health)]
        public IActionResult GetHealth()
        {

            var res = new Dictionary<string, string>();

            res.Add("Reachable", "True");

            return new OkObjectResult(res);
        }
    }
}
