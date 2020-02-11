using Parking.Mqtt.Api.Frontend.Models;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Collections.Generic;
using System.Linq;

namespace Parking.Mqtt.Api.Frontend.Presenters
{
    public class EditWebPresenter : IOutputPort<GetConfigurationResponse>, IOutputPort<SaveConfigurationResponse>
    {

        public MqttConfigurationViewModel ConfigurationViewModel { get; set; }

        public void CreateResponse(GetConfigurationResponse response)
        {
            if (response.Success)
                ConfigurationViewModel = DtoToViewModel(response.ServerConfigurations.FirstOrDefault());
        }

        public void CreateResponse(SaveConfigurationResponse response)
        {
           
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
