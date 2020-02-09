namespace Parking.Mqtt.Core.Models.MQTT.DTO
{
    public class MQTTMessage 
    {
        public MQTTMessage(string message, string topic, string clientId)
        {
            Message = message;
            Topic = topic;
            ClientId = clientId;
        }

        public string Message { get;  }

        public string Topic { get; }

        public string ClientId { get; }

       
    }
}
