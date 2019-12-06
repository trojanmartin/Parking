using Microsoft.Extensions.Options;
using Parking.Communication.Mqtt.Library;
using Parking.Communication.Mqtt.Library.Models;
using Parking.Communication.Mqtt.Library.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt.Service.Services
{
    public class MqttService : IMqttService
    {
        private readonly IMqttProvider _mqttProvider;
        private readonly IOptions<MqttOptions> _mqttOptions;

        public bool Listnening { get; private set; }

        public MqttService(IMqttProvider mqttProvider, IOptions<MqttOptions> mqttOptions)
        {
            _mqttProvider = mqttProvider;
            _mqttOptions = mqttOptions;

            _mqttProvider.MessageReceived += HandleMessage;
        }

        private async Task HandleMessage(MqttMessage data)
        {
            await Task.Run(() =>
           {
               Console.WriteLine(data.Payload);
               Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
           });
                
           
        }

        //TODO Logovanie
        public async Task<MqttResponse> BeginListeningAsync()
        {
            try
            {
                var conn = await _mqttProvider.ConnectAsync(_mqttOptions.Value);

                await _mqttProvider.SubscribeAsync(_mqttOptions.Value.TopicsToSubscribe.FirstOrDefault());

                return new MqttResponse() {  ResponseCode = MqttResponseCode.Success, Message = $"Connected and subscribing topic: {_mqttOptions.Value.TopicsToSubscribe.FirstOrDefault()}"};
            }
            catch(Exception ex)
            {
                return new MqttResponse() {  ResponseCode = MqttResponseCode.Error, Message = $"Internal error"};
            }
          
        }

        public async Task<MqttResponse> StopListeningAsync()
        {
            try
            {
               await  _mqttProvider.DisconnectAsync();
               return  new MqttResponse() { ResponseCode = MqttResponseCode.Success, Message = $"Successfuly disconected from {_mqttOptions.Value.TcpServer}" };
            }
            catch(Exception ex)
            {
                return new MqttResponse() { ResponseCode = MqttResponseCode.Error, Message = $"Internal error" };
            }
        }
    }
}
