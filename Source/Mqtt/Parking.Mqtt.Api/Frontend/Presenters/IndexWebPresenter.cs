using Parking.Mqtt.Api.Frontend.Models;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Collections.Generic;
using System.Linq;

namespace Parking.Mqtt.Api.Frontend.Presenters
{
    public class IndexWebPresenter : IOutputPort<GetConfigurationResponse>, IOutputPort<ConnectResponse>, IOutputPort<DisconnectResponse>
    {
        public List<MqttConfigurationViewModel> Configurations { get; set; } = new List<MqttConfigurationViewModel>();

        public MqttConfigurationViewModel ConnectedConfiguration { get; set; }

        public string ErrorMessage { get; set; }

        public void CreateResponse(GetConfigurationResponse response)
        {
            if (response.Success)
            {

                foreach (var config in response.ServerConfigurations)
                {                    
                    Configurations.Add(DtoToViewModel(config));
                }
            }
        }

        public void CreateResponse(ConnectResponse response)
        {
            if (response.Success)
            {
                Configurations.FirstOrDefault(x => x.Id == response.ConnectedConfiguration.Id).Connected = true;
            }
        }

        public void CreateResponse(DisconnectResponse response)
        {
            if (response.Success)
            {
                Configurations.FirstOrDefault(x => x.Connected).Connected = false;
            }
        }

        private MqttConfigurationViewModel DtoToViewModel(MQTTServerConfigurationDTO dto)
        {
            var vm = new MqttConfigurationViewModel()
            {
                Id = dto.Id,
                ClientId = dto.ClientId,
                Name = dto.ConfigurationName,
                Username = dto.Username,
                Password = dto.Password,
                TcpServer = dto.TcpServer,
                Port = dto.Port,
                KeepAlive = dto.KeepAlive,
                UseTls = dto.UseTls
            };

            var topics = new List<MqttTopicViewModel>();

            foreach (var topic in dto.Topics)
            {
                var tpc = new MqttTopicViewModel()
                {
                    QoS = topic.QoS,
                    TopicName = topic.Name
                };

                topics.Add(tpc);
            }

            vm.TopicSubscribing = topics;
            return vm;
        }
    }
}
