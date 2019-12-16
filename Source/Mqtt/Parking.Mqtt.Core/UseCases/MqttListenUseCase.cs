using Microsoft.Extensions.Logging;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.Models.Gateways.Services;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.UseCases
{
    public class MqttListenUseCase : IListenUseCase
    {
        private readonly IMqttService _mqttService;
        private readonly ILogger _logger;

        public MqttListenUseCase(ILogger<MqttListenUseCase> logger,IMqttService mqttService)
        {
            _logger = logger;

            _mqttService = mqttService;           
        }

        private async Task MessageReceivedHandlerAsync(MqttMessage data)
        {
            _logger.LogInformation("Message received: {@Message}", data);
            await Task.Run(()  =>
           {
               Console.WriteLine(data.Message);
               Console.WriteLine(data.ClientId);
           });
        }

        public async Task<bool> HandleAsync(ListenRequest request, IOutputPort<ListenResponse> response)
        {
            try
            {
                _logger.LogInformation("Calling mqttService to start listen");

                var listenResponse = await _mqttService.BeginListeningAsync(request.Topics);
                _logger.LogInformation("MqttService returned {@Response}", listenResponse);

                response.CreateResponse(listenResponse.Succes ? new ListenResponse(true,listenResponse.Errors) : new ListenResponse(false,listenResponse.Errors));
                
                if(listenResponse.Succes)
                    _mqttService.MessageReceivedAsync += MessageReceivedHandlerAsync;

                return listenResponse.Succes;
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while trying to listen topics in request: {@Request}", request);
                response.CreateResponse(new ListenResponse(false, null, ex.Message));
                return false;
            }            
        }        
    }
}
