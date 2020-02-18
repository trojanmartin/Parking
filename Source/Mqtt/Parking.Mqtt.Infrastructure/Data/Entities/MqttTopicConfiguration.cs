namespace Parking.Mqtt.Infrastructure.Data.Entities
{
    public class MqttTopicConfiguration
    {

        public enum MqttQualtiyOfService
        {
            AtMostOnce = 0,
            AtLeastOnce = 1,
            ExactlyOnce = 2
        }

        public int Id { get; set; }

        public string TopicName { get; set; }

        public MqttQualtiyOfService QoS {get;set;}
    }
}
