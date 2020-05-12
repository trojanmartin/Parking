using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using MQTTnet.Protocol;
using Parking.Mqtt.Core.Exceptions.MQTT;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models;
using Parking.Mqtt.Core.Models.Configuration;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
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
        public event Func<MQTTMessageDTO, Task> MessageReceivedAsync;

        public MqttService()
        {
            _client = new MqttFactory().CreateMqttClient();

            _client.UseApplicationMessageReceivedHandler(OnMessageReceivedAsync);            
        }

        public async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs data)
        {            
            var message = Encoding.UTF8.GetString(data.ApplicationMessage.Payload);
            await MessageReceivedAsync(new MQTTMessageDTO(message, data.ApplicationMessage.Topic, data.ClientId));           
        }
  

        /// <summary>
        /// TODO su
        /// </summary>
        /// <param name="topics"></param>
        /// <returns></returns>
        public async Task SubscribeAsync(IEnumerable<MQTTTopicConfiguration> topics)
        {

            var returnList = new List<MQTTTopicConfiguration>();
            var errorList = new List<Error>();


            var topicBuilder = new TopicFilterBuilder();

            foreach (var topic in topics)
            {
                topicBuilder.WithTopic(topic.Name).WithQualityOfServiceLevel((MqttQualityOfServiceLevel)topic.QoS);                
                      
            }
            var result = await _client.SubscribeAsync(topicBuilder.Build());            
        }



        public async Task DisconnectAsync() => await _client.DisconnectAsync();

        


        public async Task ConnectAsync(MQTTServerConfiguration configuration)
        {
            try
            {             

                var mqqtOptions = new MqttClientOptionsBuilder()
                                .WithClientId(configuration.ClientId)
                                .WithTcpServer(configuration.TcpServer, configuration.Port)
                                .WithCredentials(configuration.Username, configuration.Password)
                                .WithTls(t => t.UseTls = configuration.UseTls)
                                .WithCleanSession(configuration.CleanSession)
                                .WithKeepAlivePeriod(TimeSpan.FromSeconds(configuration.KeepAlive))
                                .Build();

                var result = await _client.ConnectAsync(mqqtOptions);

                if (result.ResultCode != MQTTnet.Client.Connecting.MqttClientConnectResultCode.Success)
                {
                    throw new ConnectionFailedException("Cannot connect to MQTTServer");
                }
            }
            catch(Exception ex)
            {
                throw new ConnectionFailedException("Error while connecting to MQTT server", ex);
            }                 
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


                return new MQTTGetStatusGateResponse(_client.IsConnected, new MQTTServerConfiguration()
                {
                    ClientId = _client.Options.ClientId,
                    TcpServer = tcpOptions.Server,
                    Port = (int)tcpOptions.Port,
                    UseTls = tcpOptions.TlsOptions.UseTls
                });                                             

            }

            return new MQTTGetStatusGateResponse(false);
        }

        public async Task Unsubscribe(IEnumerable<MQTTTopicConfiguration> topics)
        {
            var topicList = new List<string>();
            foreach(var topic in topics)
            {
                topicList.Add(topic.Name);
            }

            await _client.UnsubscribeAsync(topicList.ToArray());
        }

        
    }
}
