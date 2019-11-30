using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using MqttService.Models;

namespace MqttService
{
    public class MqttService : IMqttService
    {

        private readonly IMqttClient _client;

       

        public MqttService(IMqttClient client)
        {
            _client = client;
        }

        public async Task Connect(MqttOptions options)
        {

            var mqqtOptions = new MqttClientOptionsBuilder()
                            .WithClientId(options.ClientId)
                            .WithTcpServer(options.TcpServer, options.Port)
                            .WithCredentials(options.Username, options.Password)
                            .WithTls(t => t.UseTls = options.UseTls)
                            .Build();

            var result = await _client.ConnectAsync(mqqtOptions);
            
            
        }

        public async Task ReceivedMessage (Action<MqttApplicationMessageReceivedEventArgs> handler )
        {
            _client.UseApplicationMessageReceivedHandler(handler);
        }

        public async Task PublishMessage(MqttMessage message)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                            .WithQualityOfServiceLevel((MqttQualityOfServiceLevel)message.QoS)
                            .WithTopic(message.Topic)
                            .WithPayload(message.Payload)
                            .Build();

           var result = await _client.PublishAsync(mqttMessage, CancellationToken.None);
        }

        public async Task Subscribe(string topic)
        {
            await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).Build());
        }
    }
}
