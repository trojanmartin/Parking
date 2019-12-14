using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Parking.Mqtt.Api.Models.Requests;
using Parking.Mqtt.Api.Presenters;
using Parking.Mqtt.Api.Routing;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Controllers
{

    [ApiController]
    public class MqttController : ControllerBase
    {
        private readonly IListenUseCase _listenUseCase;
        private readonly ListenPresenter _listenPresenter;

        private readonly IConnectUseCase _connectUseCase;
        private readonly ConnectPresenter _connectPresenter;

        private readonly ILogger _logger;

        public MqttController(ILogger<MqttController> logger,IListenUseCase listenUseCase, ListenPresenter listenPresenter, IConnectUseCase connectUseCase, ConnectPresenter connectPresenter)
        {
            _logger = logger;

            _listenUseCase = listenUseCase;
            _listenPresenter = listenPresenter;

            _connectUseCase = connectUseCase;
            _connectPresenter = connectPresenter;
        }

        

        
        [HttpPost]
        [Route(MqttRouting.Listen)]
        public async Task<IActionResult> ListenAsync([FromBody]ListenApiRequest request)
        {
            _logger.LogInformation("Started proccesing ListenRequest {@ListenRequest}", request);

            //TODO validacia null
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Listen request is valid");

            var topics = new List<Parking.Mqtt.Core.Models.UseCaseRequests.Topic>();

            //TODO prerobit cez automapper
            request.Topics.ToList().ForEach((topic ) =>
           {
               var newTopic = new Parking.Mqtt.Core.Models.UseCaseRequests.Topic()
               {
                   QoS = (MqttQualityOfService)topic.QoS,
                   TopicName = topic.TopicName
               };

               topics.Add(newTopic);
           });
            
            await _listenUseCase.HandleAsync(new ListenRequest(topics), _listenPresenter);
            _logger.LogInformation("Listen request done with content {@result}", _listenPresenter.Result.Content);
            return _listenPresenter.Result;
        }



        [HttpPost]
        [Route(MqttRouting.Connect)]
        public async Task<IActionResult> ConnectAsync([FromBody]ConnectApiRequest request)
        {
            _logger.LogInformation("Started proccesing ConnectRequest {@ConnectRequest}", request);

            //TODO validacia null
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Connect request is valid");

            await _connectUseCase.HandleAsync(new ConnectRequest(request.ClientId, request.TcpServer, request.Port, request.Username, 
                                                                request.Password, request.UseTls, request.CleanSession, request.KeepAlive), _connectPresenter);

            _logger.LogInformation("Connect request done with content {@result}", _connectPresenter.Result.Content);

            return _connectPresenter.Result;
        }
    }
}
