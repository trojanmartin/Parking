using Microsoft.Extensions.Logging;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.UseCases
{
    public class MqttDisconnectUseCase : IDisconnectUseCase
    {
        private readonly IMqttService _mqttService;
        private readonly ILogger _logger;

        public MqttDisconnectUseCase(ILogger<MqttDisconnectUseCase> logger ,IMqttService mqttService)
        {
            _mqttService = mqttService;
            _logger = logger;
        }

        public async Task<bool> HandleAsync(DisconnectRequest request, IOutputPort<DisconnectResponse> response)
        {
            try
            {
                _logger.LogInformation("Trying to disconnect from broker");
                await _mqttService.DisconnectAsync();
                response.CreateResponse(new DisconnectResponse(true, "Successfuly disconected from broker "));
                _logger.LogInformation("Successfuly disconected from broker");
                return true;
            }
            catch(Exception ex)
            {
                response.CreateResponse(new DisconnectResponse(false, ex.Message));
                _logger.LogError(ex, "Error while disconnecting from broker");
                return false;
            }
        }
    }
}
