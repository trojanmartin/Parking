using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using MQTTnet.Protocol;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models;
using Parking.Mqtt.Core.Models.Gateways;
using Parking.Mqtt.Core.Models.Gateways.Services;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Mqtt.Infrastructure.Mqtt
{
    public class MqttService : IMqttService
    {
       
        private readonly IMqttClient _client;
        public event Func<MqttMessage, Task> MessageReceivedAsync;

        public MqttService()
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
        public async Task<MqttListenResponse> BeginListeningAsync(IEnumerable<Topic> topics)
        {

            var returnList = new List<string>();
            var errorList = new List<Error>();


            var topicBuilder = new TopicFilterBuilder();

            foreach (var topic in topics)
            {
                topicBuilder.WithTopic(topic.TopicName).WithQualityOfServiceLevel((MqttQualityOfServiceLevel)topic.QoS);
            }
            var result = await _client.SubscribeAsync(topicBuilder.Build());


            foreach (var res in result.Items)
            {
                if (SubscribeSucceeded(res.ResultCode))
                    returnList.Add(res.TopicFilter.Topic);
                else
                    errorList.Add(new Error(res.ResultCode.ToString(), res.TopicFilter.Topic));
            }

            return new MqttListenResponse(returnList, (returnList.Count > 0), errorList);
        }



        public async Task StopListeningAsync() => await _client.DisconnectAsync();


        public async Task<MqttConnectResponse> ConnectAsync(ConnectRequest connectRequest)
        {

            var errors = new List<Error>();

            var mqqtOptions = new MqttClientOptionsBuilder()
                            .WithClientId(connectRequest.ClientId)
                            .WithTcpServer(connectRequest.TcpServer, connectRequest.Port)
                            .WithCredentials(connectRequest.Username, connectRequest.Password)
                            .WithTls(t => t.UseTls = connectRequest.UseTls)
                            .WithCleanSession(connectRequest.CleanSession)
                            .WithKeepAlivePeriod(TimeSpan.FromSeconds(connectRequest.KeepAlive))
                            .Build();
            
            var result = await _client.ConnectAsync(mqqtOptions);

            if (result.ResultCode == MQTTnet.Client.Connecting.MqttClientConnectResultCode.Success)
            {
                return new MqttConnectResponse(true);
            }

            //error 
            var err = new Error(result.ResultCode.ToString(), result.ReasonString);
            errors.Add(err);
            return new MqttConnectResponse(false, errors);
        }

        private bool SubscribeSucceeded(MqttClientSubscribeResultCode resultCode)
        {
            return resultCode == MqttClientSubscribeResultCode.GrantedQoS0 || resultCode == MqttClientSubscribeResultCode.GrantedQoS1 || resultCode == MqttClientSubscribeResultCode.GrantedQoS2;
        }
    }
}
