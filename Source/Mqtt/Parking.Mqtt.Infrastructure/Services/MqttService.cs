using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using MQTTnet.Diagnostics;
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

    public class MqttService : IMqttService, IDisposable
    {

     

        private readonly ILogger _logger;
        private readonly IMqttClient _client;
        public event Func<MQTTMessageDTO, Task> MessageReceivedAsync;


        



        public MqttService(ILogger<MqttService> logger)
        {
            _logger = logger;

            var a = new MqttNetLogger();
            a.LogMessagePublished += A_LogMessagePublished;

            _client = new MqttFactory().CreateMqttClient(logger: a);

            _client.UseConnectedHandler(OnConnectedAsync);
            _client.UseDisconnectedHandler(OnDisconnectedAsync);
            _client.UseApplicationMessageReceivedHandler(OnMessageReceivedAsync);            
        }

        private void A_LogMessagePublished(object sender, MqttNetLogMessagePublishedEventArgs e)
        {
            _logger.LogWarning("MqttLOG {@Message}", e.LogMessage);
        }

        private async Task OnConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            _logger.LogInformation("Client Connected, {@ConnectResult}",arg.AuthenticateResult);
            //  await ConnectAsync()
        }


        private async Task OnDisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            _logger.LogError(arg.Exception,"Client disconnected");
            await _client.ReconnectAsync();
          //  await ConnectAsync()
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
                },true);                                             

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

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                _client.Dispose();
            }

            
            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        public void DisposeClient()
        {
            Dispose();
        }

        ~MqttService()
        {
            Dispose(false);
        }
    }
}
