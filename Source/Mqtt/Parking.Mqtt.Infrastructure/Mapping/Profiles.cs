using AutoMapper;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using Parking.Mqtt.Infrastructure.Data.Entities;
using static Parking.Mqtt.Infrastructure.Data.Entities.MqttTopicConfiguration;

namespace Parking.Mqtt.Infrastructure.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<MQTTTopicConfigurationDTO, MqttTopicConfiguration>()
                .ConvertUsing(x => new MqttTopicConfiguration
                {
                    QoS = (MqttQualtiyOfService)x.QoS,
                    TopicName = x.Name
                });


            CreateMap<MqttTopicConfiguration, MQTTTopicConfigurationDTO>()
             .ConvertUsing(x => new MQTTTopicConfigurationDTO(x.TopicName, (MQTTQualityOfService)x.QoS));            

            CreateMap<MQTTServerConfigurationDTO, MqttServerConfiguration>()
                .ForMember(dest => dest.Topics,
                opt => opt.MapFrom(src => src.Topics));

            CreateMap<MqttServerConfiguration, MQTTServerConfigurationDTO>()
                .ForCtorParam("clientId", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("configurationName", opt => opt.MapFrom(src => src.Name))
                .ForCtorParam("tcpServer", opt => opt.MapFrom(src => src.TCPServer))
                .ForCtorParam("port", opt => opt.MapFrom(src => src.Port))
                .ForCtorParam("username", opt => opt.MapFrom(src => src.Username))
                .ForCtorParam("password", opt => opt.MapFrom(src => src.Password))
                .ForCtorParam("useTls", opt => opt.MapFrom(src => src.UseTls))
                .ForCtorParam("cleanSession", opt => opt.MapFrom(src => src.CleanSession))
                .ForCtorParam("keepAlive", opt => opt.MapFrom(src => src.KeepAlive))
                .ForCtorParam("topics", opt => opt.MapFrom(src => src.Topics));         
               
              
               
        }
    }
}
