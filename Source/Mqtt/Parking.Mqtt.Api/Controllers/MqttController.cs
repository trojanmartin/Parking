using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parking.Mqtt.Api.Models.Requests;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Mqtt.Api.Presenters;

namespace Parking.Mqtt.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MqttController : ControllerBase
    {
        private readonly IListenUseCase _listenUseCase;
        private readonly ListenPresenter _listenPresenter;

        public MqttController(IListenUseCase listenUseCase)
        {
            _listenUseCase = listenUseCase;
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
        public async Task<IActionResult> ListenAsync([FromBody]ListenRequest request)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var topics = new List<Tuple<string, MqttQualityOfService>>();

            foreach (var req in request.Topics)
            {
                topics.Add(new Tuple<string, MqttQualityOfService>(req.TopicName, req.QoS));
            }

            await _listenUseCase.HandleAsync(new Core.Models.UseCaseRequests.ListenRequest(topics), _listenPresenter);
            return _listenPresenter.Result;
        }

        [HttpPost("stop")]
        public async Task StopListening()
        {


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
