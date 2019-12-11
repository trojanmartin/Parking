using MQTTnet;
using MQTTnet.Client;
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
        public event Func<MqttMessage, Task> MessageReceived;
        public event Func<MqttMessage, Task> MessageReceivedAsync;

        public MqttService(IMqttProvider provider)
        {
            _client = new MqttFactory().CreateMqttClient();

            _client.UseApplicationMessageReceivedHandler(OnMessageReceivedAsync);
        }

        public async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs data)
        {
            var message = Encoding.UTF8.GetString(data.ApplicationMessage.Payload);
            await MessageReceived(new MqttMessage(message, data.ApplicationMessage.Topic, data.ClientId, !data.ProcessingFailed));
        }

        

        public Task<MqttListenResponse> BeginListeningAsync(IEnumerable<Tuple<string, MqttQualityOfService>> topics)
        {
            throw new NotImplementedException();
        }

        public Task<MqttListenResponse> StopListeningAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MqttConnectResponse> ConnectAsync(ConnectRequest connectRequest)
        {
            throw new NotImplementedException();
        }
    }
}
