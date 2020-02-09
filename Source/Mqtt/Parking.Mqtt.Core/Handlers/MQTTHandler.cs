using Microsoft.Extensions.Logging;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Handlers
{
    public class MQTTHandler : IMQTTHandler
    {

        private readonly IMqttService _mqttService;
        private readonly ILogger _logger;

        public MQTTHandler(ILogger<MQTTHandler> logger, IMqttService mqttService)
        {
            _logger = logger;
            _mqttService = mqttService;
        }

        public async Task<bool> ConnectAsync(ConnectRequest connectRequest, IOutputPort<ConnectResponse> outputPort)
        {
            try
            {
                _logger.LogInformation("Calling Mqtt service to connect");
                var res = await _mqttService.ConnectAsync(connectRequest.ServerConfiguration);

                _logger.LogInformation("Mqtt service returned {@Response}", res);
                outputPort.CreateResponse(new ConnectResponse(res.Succes,res.Errors));
                return res.Succes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while connecting to broker with request {@ConnectRequest}", connectRequest);
                var err = new Error("Error while connecting to server", ex.Message);
                outputPort.CreateResponse(new ConnectResponse(false, new List<Error>() { err }));
                return false;
            }
        }

        public async Task<bool> DisconnectAsync(DisconnectRequest disconnectRequest ,IOutputPort<DisconnectResponse> outputPort)
        {
            try
            {
                _logger.LogInformation("Trying to disconnect from broker");
                await _mqttService.DisconnectAsync();
                outputPort.CreateResponse(new DisconnectResponse(true,null, "Successfuly disconected from broker "));
                _logger.LogInformation("Successfuly disconected from broker");
                return true;
            }
            catch (Exception ex)
            {
                outputPort.CreateResponse(new DisconnectResponse(false, null,ex.Message));
                _logger.LogError(ex, "Error while disconnecting from broker");
                return false;
            }
        }

        public async Task<bool> SubscribeAsync(SubscribeRequest subscribeRequest, IOutputPort<SubscribeResponse> outputPort)
        {
            try
            {
                _logger.LogInformation("Calling mqttService to start listen");

                var subscribeResponse = await _mqttService.SubscribeAsync(subscribeRequest.Topics);
                _logger.LogInformation("MqttService returned {@Response}", subscribeResponse);

                outputPort.CreateResponse(subscribeResponse.Succes ? new SubscribeResponse(true, subscribeResponse.Errors) : new SubscribeResponse(false, subscribeResponse.Errors));

                if (subscribeResponse.Succes)
                    _mqttService.MessageReceivedAsync += MessageReceivedHandlerAsync;

                return subscribeResponse.Succes;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to listen topics in request: {@Request}", subscribeRequest);
                outputPort.CreateResponse(new SubscribeResponse(false));
                return false;
            }
        }

        private async Task MessageReceivedHandlerAsync(MQTTMessage message)
        {
            await  Task.Run(() =>
           {
               Console.WriteLine(message.Message);
           });
            
        }
    }
}
