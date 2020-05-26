using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parking.Mqtt.Core.Models.MQTT.DataMessage
{
    public partial class RawSensorData
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("deveui")]
        public string Deveui { get; set; }
    }
}
