using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Parking.Mqtt.Api.Models.Requests;
using Parking.Mqtt.Api.Presenters;
using Parking.Mqtt.Api.Routing;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Controllers
{

    [ApiController]
    public class MqttController : ControllerBase
    {
        private readonly DisconnectPresenter _disconnectPresenter;
        private readonly ConnectPresenter _connectPresenter;
        private readonly SubscribePresenter _subscribePresenter;
      

        private readonly IMQTTHandler _handler;
        private readonly ILogger _logger;

        public MqttController(IMQTTHandler handler, ILogger<MqttController> logger, DisconnectPresenter disconnectPresenter, ConnectPresenter connectPresenter, SubscribePresenter subscribePresenter)
        {
            _handler = handler;
            _logger = logger;
            _disconnectPresenter = disconnectPresenter;
            _connectPresenter = connectPresenter;
            _subscribePresenter = subscribePresenter;
        }

        [HttpPost]
        [Route(ApiRouting.Subscribe)]
        public async Task<IActionResult> SubscribeAsync([FromBody]ListenApiRequest request)
        {
            _logger.LogInformation("Started proccesing ListenRequest {@ListenRequest}", request);

            //TODO validacia null
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Listen request is valid");

            var topics = new List<MQTTTopicConfigurationDTO>();

            //TODO prerobit cez automapper
            request?.Topics.ToList().ForEach((topic ) =>
           {
               var newTopic = new MQTTTopicConfigurationDTO(topic.TopicName, (MQTTQualityOfService)topic.QoS);             

               topics.Add(newTopic);
           });
            
            await _handler.SubscribeAsync(new SubscribeRequest(topics), _subscribePresenter);
            _logger.LogInformation("Listen request done with content {@result}", _subscribePresenter.Result.Content);
            return _subscribePresenter.Result;
        }



        [HttpPost]
        [Route(ApiRouting.Connect)]
        public async Task<IActionResult> ConnectAsync([FromBody]ConnectApiRequest request)
        {
            _logger.LogInformation("Started proccesing ConnectRequest {@ConnectRequest}", request);

            //TODO validacia null
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Connect request is valid");

            await _handler.ConnectAsync(new ConnectRequest(new MQTTServerConfigurationDTO(request?.ClientId, request.TcpServer, request.Port, request.Username, 
                                                                request.Password, request.UseTls, request.CleanSession, request.KeepAlive)), _connectPresenter);

            _logger.LogInformation("Connect request done with content {@result}", _connectPresenter.Result.Content);

            return _connectPresenter.Result;
        }

        [HttpPost]
        [Route(ApiRouting.Disconnect)]
        public async Task<IActionResult> DisconnectAsync()
        {
            _logger.LogInformation("Started proccesing DisconnectRequest");

            //TODO validacia null
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Disconnect request is valid");

            await _handler.DisconnectAsync(new DisconnectRequest(), _disconnectPresenter);

            return _disconnectPresenter.Result;
        }      
    }
}
