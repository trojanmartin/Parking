using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.Models.Gateways.Services;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.UseCases
{
    public class MqttListenUseCase : IListenUseCase, IMessageListener
    {
        private readonly IMqttService _mqttService;

        public MqttListenUseCase(IMqttService mqttService)
        {
            _mqttService = mqttService;


            _mqttService.MessageReceivedAsync += MessageReceivedHandlerAsync;
        }

        private async Task MessageReceivedHandlerAsync(MqttMessage data)
        {
            await Task.Run(() =>
           {

               Console.WriteLine(data.Message);
               Console.WriteLine(data.ClientId);
           });
        }

        public async Task<bool> HandleAsync(ListenRequest request, IOutputPort<ListenResponse> response)
        {
            try
            {
                var listenResponse = await _mqttService.BeginListeningAsync(request.Topics);
                response.CreateResponse(listenResponse.Succes ? new ListenResponse(true,listenResponse.Errors) : new ListenResponse(false,listenResponse.Errors));
                return listenResponse.Succes;
            }

            catch(Exception ex)
            {
                response.CreateResponse(new ListenResponse(false, null, ex.Message));
                return false;
            }            
        }

        public Task HandleAsync(MqttMessage message, IOutputPort<MqttMessage> response = null)
        {
            throw new NotImplementedException();
        }
    }
}
