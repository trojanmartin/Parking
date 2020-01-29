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
    public class MqttGetStatusUseCase : IGetStatusUseCase
    {

        private readonly IMqttService _mqttService;
        private readonly ILogger _logger;

        public MqttGetStatusUseCase(IMqttService mqttService, ILogger<MqttGetStatusUseCase> logger)
        {
            _mqttService = mqttService;
            _logger = logger;
        }

        public async Task<bool> HandleAsync(GetStatusRequest request, IOutputPort<GetStatusResponse> response)
        {
            try
            {
                var result = await _mqttService.GetStatusAsync();
                response.CreateResponse(new GetStatusResponse(result, true));
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }                       
        }
    }
}
