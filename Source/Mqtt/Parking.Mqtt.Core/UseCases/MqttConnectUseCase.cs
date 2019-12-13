﻿using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.Models;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.UseCases
{
    public class MqttConnectUseCase : IConnectUseCase
    {

        private readonly IMqttService _mqttService;

        public MqttConnectUseCase(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }

        public async Task<bool> HandleAsync(ConnectRequest request, IOutputPort<ConnectResponse> response)
        {

            try
            {
                var res = await _mqttService.ConnectAsync(request);
                response.CreateResponse(new ConnectResponse(res.Succes, res.Errors, "Succesfully conected to broker"));
                return res.Succes;
            }                                 
            catch(Exception ex)
            {
                var err = new Error("Error while connecting to server", ex.Message);
                response.CreateResponse(new ConnectResponse(false, new List<Error>() { err }));
                return false;
            }

        }
    }
}