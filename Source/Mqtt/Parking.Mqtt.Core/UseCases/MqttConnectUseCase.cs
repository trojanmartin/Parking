using Microsoft.Extensions.Logging;
using Parking.Mqtt.Core.Interfaces;
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
        private readonly ILogger _logger;

        public MqttConnectUseCase(ILogger<MqttConnectUseCase> logger,IMqttService mqttService)
        {
            _logger = logger;
            _mqttService = mqttService;
        }

        public async Task<bool> HandleAsync(ConnectRequest request, IOutputPort<ConnectResponse> response)
        {
            
            try
            {
                _logger.LogInformation("Calling Mqtt service to connect");
                var res = await _mqttService.ConnectAsync(request);

                _logger.LogInformation("Mqtt service returned {@Response}",res);
                response.CreateResponse(new ConnectResponse(res.Succes, res.Errors, "Succesfully conected to broker"));
                return res.Succes;
            }                                 
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while connecting to broker with request {@ConnectRequest}", request);
                var err = new Error("Error while connecting to server", ex.Message);
                response.CreateResponse(new ConnectResponse(false, new List<Error>() { err }));
                return false;
            }

        }
    }
}
