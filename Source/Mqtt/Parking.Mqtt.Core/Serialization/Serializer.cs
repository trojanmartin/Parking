using System.Text.Json;

namespace Parking.Mqtt.Core.Serialization
{
    public static class Serializer
    {
        private static readonly JsonSerializerOptions Settings = new JsonSerializerOptions()
        {
            WriteIndented = false
        };

        public static string SerializeObjectToJson(object o) => JsonSerializer.Serialize(o, Settings);

        public static T DeserializeToObject<T>(string json) => JsonSerializer.Deserialize<T>(json);
    }
}
