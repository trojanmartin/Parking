using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using MQTTnet.Protocol;
using Parking.Communication.Mqtt.Library.Models;
using Parking.Communication.Mqtt.Library.Models.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt.Library
{
    public class MqttProvider : IMqttProvider
    {

        public event Func<MqttMessage, Task> MessageReceived;


        private readonly IMqttClient _client;
        private MqttOptions _mqttOptions;


        public MqttProvider()
        {

            var client = new MqttFactory().CreateMqttClient();

            _client = client;

            _client.UseApplicationMessageReceivedHandler(OnMessageReceivedAsync);

            _client.UseDisconnectedHandler(OnDisconnect);
        }




        //TODO doplnit dalsie data do objektu MqttMessage
        public async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs data)
        {
            var newData = new MqttMessage()
            {
                Payload = Encoding.UTF8.GetString(data.ApplicationMessage.Payload),
                Topic = data.ApplicationMessage.Topic
            };

            await MessageReceived(newData);
        }

        public async Task<MqttResponse> PublishMessageAsync(MqttMessage message)
        {
            var mqttMessage = new MqttApplicationMessageBuilder()
                            .WithQualityOfServiceLevel((MqttQualityOfServiceLevel)message.QoS)
                            .WithTopic(message.Topic)
                            .WithPayload(message.Payload)
                            .Build();

            var result = await _client.PublishAsync(mqttMessage, CancellationToken.None);

            var response = new MqttResponse()
            {
                ResponseCode = (result.ReasonCode == MqttClientPublishReasonCode.Success) ? MqttResponseCode.Success : MqttResponseCode.Error
            };

            return response;
        }

        /// <summary>
        /// Subscribe to given topic, Before this function, client must be connected
        /// </summary>
        /// <param name="topic">Topic to subscribe</param>
        /// <param name="qoS">Quality of service, default is AtLeastOnce</param>
        /// <returns></returns>
        public async Task SubscribeAsync(string topic, MqttMessageQoS qoS = MqttMessageQoS.AtLeastOnce)
        {
            await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).WithQualityOfServiceLevel((MqttQualityOfServiceLevel)qoS).Build());
        }

        private async Task OnDisconnect(MqttClientDisconnectedEventArgs data)
        {
            if (!_mqttOptions.ReconnectOptions.Reconnect)
                return;
            try
            {
                await Task.Delay(_mqttOptions.ReconnectOptions.After);
                await ConnectAsync(_mqttOptions);
            }

            catch (Exception ex)
            {
                throw new ReconnectFailedException($"Cannot reconnect to {_mqttOptions.TcpServer}", ex);
            }
        }

        public async Task DisconnectAsync()
        {
            await _client.UnsubscribeAsync();
            await _client.DisconnectAsync();
        }

        public async Task<MqttResponse> ConnectAsync(MqttOptions options)
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

            var response = new MqttResponse()
            {
                ResponseCode = (result.ResultCode == MqttClientConnectResultCode.Success) ? MqttResponseCode.Success : MqttResponseCode.Error
            };


            return response;
        }        

     
    }
}
