using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Parking.Models.Mqtt;
using Parking.Models.Mqtt.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt.Library
{
    public class MqttProvider : IMqttProvider
    {

        public event Func<MqttMessage,Task> MessageReceived = async (message) =>  {  };


        private readonly IMqttClient _client;
        private  MqttOptions _mqttOptions;


        public MqttProvider()
        {

            var client = new MqttFactory().CreateMqttClient();

            _client = client;
            


            _client.UseApplicationMessageReceivedHandler((receivedMessage) =>
            {
                OnMessageReceived(receivedMessage);
            });

            _client.UseDisconnectedHandler((data) =>
           {
               OnDisconnect(data, _mqttOptions.ReconnectOptions);
           });

        }

        public async Task Connect(MqttOptions options)
        {
            _mqttOptions = options;


            var mqqtOptions = new MqttClientOptionsBuilder()
                            .WithClientId(_mqttOptions.ClientId)
                            .WithTcpServer(_mqttOptions.TcpServer, _mqttOptions.Port)
                            .WithCredentials(_mqttOptions.Username, _mqttOptions.Password)
                            .WithTls(t => t.UseTls = _mqttOptions.UseTls)
                            .WithCleanSession(_mqttOptions.CleanSession)
                            .WithKeepAlivePeriod(TimeSpan.FromSeconds(_mqttOptions.KeepAlive))
                            .Build();

            var result = await _client.ConnectAsync(mqqtOptions);
        }


        //TODO doplnit dalsie data do objektu MqttMessage
        public async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs data)
        {
            var newData = new MqttMessage()
            {
                Payload = Encoding.UTF8.GetString(data.ApplicationMessage.Payload),
                Topic = data.ApplicationMessage.Topic
            };

            MessageReceived(newData);
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

        /// <summary>
        /// Subscribe to given topic, Before this function, client must be connected
        /// </summary>
        /// <param name="topic">Topic to subscribe</param>
        /// <param name="qoS">Quality of service, default is AtLeastOnce</param>
        /// <returns></returns>
        public async Task Subscribe(string topic, MqttMessageQoS qoS = MqttMessageQoS.AtLeastOnce)
        {
            await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).WithQualityOfServiceLevel((MqttQualityOfServiceLevel)qoS).Build());

        }

        private async void OnDisconnect(MqttClientDisconnectedEventArgs data, ReconnectOptions reconnectOptions)
        {
            if (!reconnectOptions.Reconnect)
                return;

            _client.UseDisconnectedHandler(async (data) =>
           {

               try
               {
                   await Task.Delay(reconnectOptions.After);
                   await Connect(_mqttOptions);
               }

               catch (Exception ex)
               {
                   throw new ReconnectFailedException($"Cannot reconnect to {_mqttOptions.TcpServer}", ex);
               }


           });
        }
    }
}
