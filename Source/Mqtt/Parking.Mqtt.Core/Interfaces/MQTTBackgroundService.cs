using Microsoft.Extensions.Hosting;
using Parking.Mqtt.Core.Interfaces.Base;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.Configuration;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces
{
    public abstract class MQTTBackgroundService : BackgroundService
    {
        private readonly MQTTServerConfiguration _serverConfig;
        private readonly IEnumerable<MQTTTopicConfiguration> _topicsConfig;
        private readonly IMqttService _client;

     

        public MQTTBackgroundService(IMqttService client, MQTTServerConfiguration serverConfig, IEnumerable<MQTTTopicConfiguration> topicsConfig)
        {
            _serverConfig = serverConfig;
            _topicsConfig = topicsConfig;
            _client = client;
        }

        public async Task ConfigureAsync()
        {
            await ConnectAsync();
            await SubscribeAsync();
        }

        private async Task ConnectAsync()
        {
            await _client.ConnectAsync(_serverConfig);
            _client.MessageReceivedAsync += HandleMessageAsync;
        }

        public abstract Task HandleMessageAsync(MQTTMessageDTO arg);


        private async Task SubscribeAsync()
        {
            await _client.SubscribeAsync(_topicsConfig);
        }     
                         

        protected virtual async Task DisconnectAsync()
        {
            await _client.DisconnectAsync();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {           
            return Task.Run(async () =>
            {
                await ConfigureAsync();


                while (true)
                {
                    await Task.Delay(100);
                    if (stoppingToken.IsCancellationRequested)
                    {
                        await DisconnectAsync();
                        break;
                    }
                }
            });            
        }
    }
}
