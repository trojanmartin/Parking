using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.Gateways;
using Parking.Mqtt.Core.Models.Gateways.Services;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Mqtt.Infrastructure.Mqtt
{
    public class MqttService : IMqttService
    {
        private readonly IMqttProvider _provider;
        private readonly IMqttClient _client;      
        public event Func<MqttMessage, Task> MessageReceivedAsync;

        public MqttService(IMqttProvider provider)
        {
            _client = new MqttFactory().CreateMqttClient();

            _client.UseApplicationMessageReceivedHandler(OnMessageReceivedAsync);
        }

        public async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs data)
        {
            var message = Encoding.UTF8.GetString(data.ApplicationMessage.Payload);
            await MessageReceivedAsync(new MqttMessage(message, data.ApplicationMessage.Topic, data.ClientId, !data.ProcessingFailed));
        }

        
        /// <summary>
        /// TODO su
        /// </summary>
        /// <param name="topics"></param>
        /// <returns></returns>
        public async Task<MqttListenResponse> BeginListeningAsync(IEnumerable<Tuple<string, MqttQualityOfService>> topics)
        {
            foreach(var topic in topics)
            {
              var result =  await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic.Item1).WithQualityOfServiceLevel((MqttQualityOfServiceLevel)topic.Item2).Build());
            }
            //TODO
            return new MqttListenResponse(new List<string>{ "test"}, "", true);
          
        }

        public async Task StopListeningAsync()=>  await _client.DisconnectAsync();
        

        public async Task<MqttConnectResponse> ConnectAsync(ConnectRequest connectRequest)
        {
            var mqqtOptions = new MqttClientOptionsBuilder()
                            .WithClientId(connectRequest.ClientId)
                            .WithTcpServer(connectRequest.TcpServer, connectRequest.Port)
                            .WithCredentials(connectRequest.Username, connectRequest.Password)
                            .WithTls(t => t.UseTls = connectRequest.UseTls)
                            .WithCleanSession(connectRequest.CleanSession)
                            .WithKeepAlivePeriod(TimeSpan.FromSeconds(connectRequest.KeepAlive))
                            .Build();

            var result = await _client.ConnectAsync(mqqtOptions);

            //TODO
            return (result.ResultCode == MQTTnet.Client.Connecting.MqttClientConnectResultCode.Success) ? new MqttConnectResponse(true) : new MqttConnectResponse(false);
        }
    }
}
