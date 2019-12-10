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
    public class ListenUseCase : IListenUseCase
    {
        private readonly IMqttService _mqttService;

        public ListenUseCase(IMqttService mqttService)
        {
            _mqttService = mqttService;
        }


        public async Task<bool> HandleAsync(ListenRequest request, IOutputPort<ListenResponse> response)
        {
            try
            {
                var listenResponse = await _mqttService.BeginListeningAsync();
                await response.CreateResponseAsync(listenResponse.Succes ? new ListenResponse(true) : new ListenResponse(false);
                return listenResponse.Succes;
            }

            catch(Exception ex)
            {
               await  response.CreateResponseAsync(new ListenResponse(false));
                return false;
            }
            
        }
    }
}
