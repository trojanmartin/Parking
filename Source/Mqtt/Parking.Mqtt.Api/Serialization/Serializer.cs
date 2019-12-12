using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Serialization
{
    public static class Serializer
    {
        private static readonly JsonSerializerOptions Settings = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        public static string SerializeObjectToJson(object o) => JsonSerializer.Serialize(o, Settings);
       

    }

}

