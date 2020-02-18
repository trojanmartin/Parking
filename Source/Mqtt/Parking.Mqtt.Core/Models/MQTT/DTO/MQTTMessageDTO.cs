namespace Parking.Mqtt.Core.Models.MQTT.DTO
{
    public class MQTTMessageDTO 
    {
        public MQTTMessageDTO(string payload, string topic, string clientId)
        {
            Payload = payload;
            Topic = topic;
            ClientId = clientId;
        }

        public string Payload { get;  }

        public string Topic { get; }

        public string ClientId { get; }       
    }
}
