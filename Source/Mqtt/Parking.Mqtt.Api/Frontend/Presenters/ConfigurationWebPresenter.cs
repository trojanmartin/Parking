using Parking.Mqtt.Api.Frontend.Models;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Collections.Generic;

namespace Parking.Mqtt.Api.Frontend.Presenters
{
    public class ConfigurationWebPresenter : IOutputPort<GetConfigurationResponse>, IOutputPort<ConnectResponse>
    {
        public List<MqttConfigurationViewModel> Configurations { get; set; } = new List<MqttConfigurationViewModel>();

        public MqttConfigurationViewModel ConnectedConfiguration { get; set; }

        public string ErrorMessage { get; set; }

        public void CreateResponse(GetConfigurationResponse response)
        {
            if (response.Success)
            {

                foreach(var config in response.ServerConfigurations)
                {
                    var cnfg = new MqttConfigurationViewModel()
                    {
                        Id = config.Id,
                        ClientId = config.ClientId,
                        Name = config.ConfigurationName,
                        Username = config.Username,
                        Password = config.Password,
                        TcpServer = config.TcpServer,                        
                        Port = config.Port,
                        KeepAlive = config.KeepAlive,
                        UseTls = config.UseTls
                    };

                    var topics = new List<MqttTopicViewModel>();

                    foreach (var topic in config.Topics)
                    {
                        var tpc = new MqttTopicViewModel()
                        {
                            QoS = topic.QoS,
                            TopicName = topic.Name
                        };

                        topics.Add(tpc);
                    }

                    cnfg.TopicSubscribing = topics;
                    Configurations.Add(cnfg);
                }

                
            }
        }

        public void CreateResponse(ConnectResponse response)
        {
            
        }
    }
}
