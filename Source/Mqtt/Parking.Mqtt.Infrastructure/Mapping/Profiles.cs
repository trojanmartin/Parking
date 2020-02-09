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
            CreateMap<MQTTTopicConfiguration, MqttTopicConfiguration>()
                .ConvertUsing(x => new MqttTopicConfiguration
                {
                    QoS = (MqttQualtiyOfService)x.QoS,
                    TopicName = x.Name
                });


            CreateMap<MqttTopicConfiguration, MQTTTopicConfiguration>()
             .ConvertUsing(x => new MQTTTopicConfiguration(x.TopicName, (MQTTQualityOfService)x.QoS));            

            CreateMap<MQTTServerConfiguration, MqttServerConfiguration>()
                .ForMember(dest => dest.Topics,
                opt => opt.MapFrom(src => src.Topics));

            CreateMap<MqttServerConfiguration, MQTTServerConfiguration>()
               .ForMember(dest => dest.Topics,
               opt => opt.MapFrom(src => src.Topics));         
        }
    }
}
