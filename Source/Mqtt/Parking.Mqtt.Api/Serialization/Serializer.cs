using System.Text.Json;

namespace Parking.Mqtt.Api.Serialization
{
    public static class Serializer
    {
        private static readonly JsonSerializerOptions Settings = new JsonSerializerOptions()
        {
            WriteIndented = false
        };

        public static string SerializeObjectToJson(object o) => JsonSerializer.Serialize(o, Settings);     

    }

}

