using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using MQTTnet.Protocol;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models;
using Parking.Mqtt.Core.Models.Gateways;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Mqtt.Infrastructure.Mqtt
{
    public class MqttService : IMqttService
    {       
       
        private readonly IMqttClient _client;
        public event Func<MQTTMessage, Task> MessageReceivedAsync;

        public MqttService()
        {
            _client = new MqttFactory().CreateMqttClient();

            _client.UseApplicationMessageReceivedHandler(OnMessageReceivedAsync);            
        }

        public async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs data)
        {            
            var message = Encoding.UTF8.GetString(data.ApplicationMessage.Payload);
            await MessageReceivedAsync(new MQTTMessage(message, data.ApplicationMessage.Topic, data.ClientId));           
        }


        /// <summary>
        /// TODO su
        /// </summary>
        /// <param name="topics"></param>
        /// <returns></returns>
        public async Task<MQTTSubscribeGateResponse> SubscribeAsync(IEnumerable<MQTTTServerConfiguration> topics)
        {

            var returnList = new List<MQTTTServerConfiguration>();
            var errorList = new List<Error>();


            var topicBuilder = new TopicFilterBuilder();

            foreach (var topic in topics)
            {
                topicBuilder.WithTopic(topic.Name).WithQualityOfServiceLevel((MqttQualityOfServiceLevel)topic.QoS);                
                      
            }
            var result = await _client.SubscribeAsync(topicBuilder.Build());


            foreach (var res in result.Items)
            {
                if (SubscribeSucceeded(res.ResultCode))
                    returnList.Add(new MQTTTServerConfiguration(res.TopicFilter.Topic, (MQTTQualityOfService)res.TopicFilter.QualityOfServiceLevel));
                else
                    errorList.Add(new Error(res.ResultCode.ToString(), res.TopicFilter.Topic));
            }

            return new MQTTSubscribeGateResponse(returnList, (returnList.Count > 0), errorList);
        }



        public async Task DisconnectAsync() => await _client.DisconnectAsync();


        public async Task<MQTTConnectGateResponse> ConnectAsync(MQTTServerConfiguration configuration)
        {

            var errors = new List<Error>();

            var mqqtOptions = new MqttClientOptionsBuilder()
                            .WithClientId(configuration.ClientId)
                            .WithTcpServer(configuration.TcpServer, configuration.Port)
                            .WithCredentials(configuration.Username, configuration.Password)
                            .WithTls(t => t.UseTls = configuration.UseTls)
                            .WithCleanSession(configuration.CleanSession)
                            .WithKeepAlivePeriod(TimeSpan.FromSeconds(configuration.KeepAlive))
                            .Build();
            
            var result = await _client.ConnectAsync(mqqtOptions);

            if (result.ResultCode == MQTTnet.Client.Connecting.MqttClientConnectResultCode.Success)
            {
                return new MQTTConnectGateResponse(true);
            }

            //error 
            var err = new Error(result.ResultCode.ToString(), result.ReasonString);
            errors.Add(err);
            return new MQTTConnectGateResponse(false, errors);
        }

        private bool SubscribeSucceeded(MqttClientSubscribeResultCode resultCode)
        {
            return resultCode == MqttClientSubscribeResultCode.GrantedQoS0 || resultCode == MqttClientSubscribeResultCode.GrantedQoS1 || resultCode == MqttClientSubscribeResultCode.GrantedQoS2;
        }

  

        public async Task<MQTTGetStatusGateResponse> GetStatusAsync()
        {
            if (_client?.Options != null)
            {
                var tcpOptions = (MqttClientTcpOptions)_client.Options.ChannelOptions;

                return new MQTTGetStatusGateResponse(_client.IsConnected, new MQTTServerConfiguration(_client.Options.ClientId, tcpOptions.Server, (int)tcpOptions.Port, "", "", false, false, 100), true);                              
                    
            }

            return new MQTTGetStatusGateResponse(false);
        }        
    }
}
