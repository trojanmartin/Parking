using Microsoft.Extensions.Logging;
using Parking.Mqtt.Core.Exceptions;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models;
using Parking.Mqtt.Core.Models.Errors;
using Parking.Mqtt.Core.Models.MQTT;
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
        private readonly IMQTTConfigurationRepository _repo;
        private readonly IDataReceivedHandler _dataHandler;
        private readonly ILogger _logger;

        public MQTTHandler(ILogger<MQTTHandler> logger, IMqttService mqttService, IMQTTConfigurationRepository repo, IDataReceivedHandler dataHandler)
        {
            _logger = logger;
            _mqttService = mqttService;
            _repo = repo;
            _dataHandler = dataHandler;
        }

        public async Task<bool> ConnectAsync(ConnectRequest connectRequest, IOutputPort<ConnectResponse> outputPort)
        {
            try
            {
                _logger.LogInformation("Calling Mqtt service to connect");
                var res = await _mqttService.ConnectAsync(connectRequest.ServerConfiguration);

                _logger.LogInformation("Mqtt service returned {@Response}", res);
                outputPort.CreateResponse(new ConnectResponse(connectRequest.ServerConfiguration, res.Succes));
                return res.Succes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while connecting to broker with request {@ConnectRequest}", connectRequest);
                outputPort.CreateResponse(new ConnectResponse(false, new List<Error>() { GlobalErrors.UnexpectedError }));
                return false;
            }
        }

        public async Task<bool> ConnectAsync(int configurationId, IOutputPort<ConnectResponse> outputPort)
        {
            try
            {
                var configuration = await _repo.GetConfigurationAsync(configurationId);

                if (configuration != null)
                    return await ConnectAsync(new ConnectRequest(configuration), outputPort);

                outputPort.CreateResponse(new ConnectResponse(false, new[] { new Error(GlobalErrorCodes.NotFound, $"Configuration with id {configurationId} does not exist") }));
                return false;
            }
            catch(NotFoundException ex)
            {
                outputPort.CreateResponse(new ConnectResponse(false, new[] { new Error(GlobalErrorCodes.NotFound, $"Configuration with id {configurationId} does not exist") }));
                return false;
            }
            catch(Exception ex)
            {
                outputPort.CreateResponse(new ConnectResponse(false, new[] {  GlobalErrors.UnexpectedError }));
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

        private async Task MessageReceivedHandlerAsync(MQTTMessageDTO message)
        {
            await _dataHandler.ProccesMQTTMessage(message);
        }



        public async Task<bool> GetConfigurationAsync(GetConfigurationRequest configurationRequest, IOutputPort<GetConfigurationResponse> outputPort)
        {
           _logger.LogInformation("GetConfigurationAsync invoked with {@Request}", configurationRequest);

           try
            {
                if(configurationRequest.Id == null)
                {
                    var res = await _repo.GetConfigurationsAsync();
                    outputPort.CreateResponse(new GetConfigurationResponse(res, true));
                    
                }
                else
                {
                    var res =await  _repo.GetConfigurationAsync((int)configurationRequest.Id);
                    outputPort.CreateResponse(new GetConfigurationResponse(new List<MQTTServerConfigurationDTO>() { res }, true));                   
                }
                _logger.LogInformation("GetConfigurationAsync succesfull");
                return true;
            }
            catch (NotFoundException ex)
            {
                outputPort.CreateResponse(new GetConfigurationResponse(false, new[] { new Error(GlobalErrorCodes.NotFound, ex.Message)}));
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                outputPort.CreateResponse(new GetConfigurationResponse(false, new List<Error>() { GlobalErrors.UnexpectedError }));
                return false;
            }
        }

        public async Task<bool> SaveConfigurationAsync(SaveConfigurationRequest saveConfigurationRequest, IOutputPort<SaveConfigurationResponse> outputPort)
        {
            _logger.LogInformation("SaveConfigurationAsync invoked with {@Request}", saveConfigurationRequest);
           
            try
            {
                await _repo.CreateConfigurationAsync(saveConfigurationRequest.MqttServerConfiguration);
                outputPort.CreateResponse(new SaveConfigurationResponse(true));
                _logger.LogInformation("SaveConfigurationAsync succesfull");
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                outputPort.CreateResponse(new SaveConfigurationResponse(false, new List<Error>() { GlobalErrors.UnexpectedError }));
                return false;
            }
        }

        public async Task<bool> UpdateConfigurationAsync(SaveConfigurationRequest updateConfigurationRequest, IOutputPort<SaveConfigurationResponse> outputPort)
        {
            _logger.LogInformation("UpdateConfigurationAsync invoked with {@Request}", updateConfigurationRequest);

            try
            {
                await _repo.UpdateConfigurationAsync(updateConfigurationRequest.MqttServerConfiguration);
                outputPort.CreateResponse(new SaveConfigurationResponse(true));
                _logger.LogInformation("UpdateConfigurationAsync succesfull");
                return true;
            }
            catch(NotFoundException ex)
            {
                outputPort.CreateResponse(new SaveConfigurationResponse(false, new[] { new Error(GlobalErrorCodes.NotFound, ex.Message) }));
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                outputPort.CreateResponse(new SaveConfigurationResponse(false, new List<Error>() { GlobalErrors.UnexpectedError }));
                return false;
            }
        }

    }
}
