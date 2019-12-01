using Microsoft.Extensions.Options;
using Parking.Models.Mqtt;
using Parking.Models.Mqtt.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt.Handlers
{
    public class MessageHandler : IMessageHandler
    {

        private readonly IMqttProvider _mqttProvider;
        private readonly IOptions<MqttOptions> _mqttOptions;

        public bool Listnening { get; private set; }

        public MessageHandler(IMqttProvider mqttProvider, IOptions<MqttOptions> mqttOptions)
        {
            _mqttProvider = mqttProvider;
            _mqttOptions = mqttOptions;

            _mqttProvider.MessageReceived += MessageReceived;          

            
        }

        private async Task MessageReceived(MqttMessage data)
        {

            await Task.Run(() =>
           {
               Console.WriteLine(data.Payload);
               Console.WriteLine(Task.CurrentId);               
           });
        }

        public async Task BeginListening()
        {        

            await _mqttProvider.Connect(_mqttOptions.Value);

            await _mqttProvider.Subscribe(_mqttOptions.Value.TopicsToSubscribe.FirstOrDefault());  

            while (Listnening)
            {

            }
        }

        public void StopListening()
        {
            Listnening = false;
        }


    }
}
