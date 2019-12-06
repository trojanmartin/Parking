using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parking.Communication.Mqtt.Service.Services;

namespace Parking.Communication.Mqtt.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MqttController : ControllerBase
    {
        private readonly IMqttService _mqttService;

        public MqttController(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }

        // GET: api/Mqtt
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Mqtt/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Mqtt
        [HttpPost("listen")]        
        public async Task<IActionResult> ListenAsync()
        {
            var response = await _mqttService.BeginListeningAsync();

            if (response.ResponseCode == Models.Mqtt.MqttResponseCode.Success)
                return Ok(response);

            return BadRequest(response);
        }
        
        [HttpPost("stop")]
        public async Task<IActionResult> StopListening()
        {
            var response = await _mqttService.StopListeningAsync();

           if (response.ResponseCode == Models.Mqtt.MqttResponseCode.Success)
                return Ok(response); 
                    
            return BadRequest(response);
            
        }

        // PUT: api/Mqtt/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
