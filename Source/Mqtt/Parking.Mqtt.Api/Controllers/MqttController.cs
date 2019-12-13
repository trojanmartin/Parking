﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Parking.Mqtt.Api.Models.Requests;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parking.Mqtt.Api.Presenters;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Api.Routing;

namespace Parking.Mqtt.Service.Controllers
{
    
    [ApiController]
    public class MqttController : ControllerBase
    {
        private readonly IListenUseCase _listenUseCase;
        private readonly ListenPresenter _listenPresenter;

        private readonly IConnectUseCase _connectUseCase;
        private readonly ConnectPresenter _connectPresenter;

        public MqttController(IListenUseCase listenUseCase, ListenPresenter listenPresenter, IConnectUseCase connectUseCase, ConnectPresenter connectPresenter)
        {
            _listenUseCase = listenUseCase;
            _listenPresenter = listenPresenter;

            _connectUseCase = connectUseCase;
            _connectPresenter = connectPresenter;
        }

        

        
        [HttpPost]
        [Route(MqttRouting.Listen)]
        public async Task<IActionResult> ListenAsync([FromBody]ListenApiRequest request)
        {
            //TODO validacia null
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

          var topics = new List<Tuple<string, MqttQualityOfService>>();

            foreach (var req in request.Topics)
            {
                topics.Add(new Tuple<string, MqttQualityOfService>(req.TopicName, req.QoS));
            }

            await _listenUseCase.HandleAsync(new ListenRequest(topics), _listenPresenter);
            return _listenPresenter.Result;
        }



        [HttpPost]
        [Route(MqttRouting.Connect)]
        public async Task<IActionResult> ConnectAsync([FromBody]ConnectApiRequest request)
        {
            //TODO validacia null
            if (!ModelState.IsValid)
                return BadRequest(ModelState);           

            await _connectUseCase.HandleAsync(new ConnectRequest(request.ClientId, request.TcpServer, request.Port, request.Username, 
                                                                request.Password, request.UseTls, request.CleanSession, request.KeepAlive), _connectPresenter);

            return _connectPresenter.Result;
        }
    }
}
