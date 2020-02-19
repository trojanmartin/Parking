using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Parking.Mqtt.Core.Models.MQTT.DataMessage
{
    public class SensorData
    {
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("streamId")]
        public string StreamId { get; set; }

        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; set; }

        [JsonPropertyName("extra")]
        public Extra Extra { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("value")]
        public Value Value { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
    }

    public class Extra
    {
    }

    public class Location
    {
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("alt")]
        public int Alt { get; set; }

        [JsonPropertyName("accuracy")]
        public int Accuracy { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("connector")]
        public string Connector { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("encoding")]
        public string Encoding { get; set; }

        [JsonPropertyName("group")]
        public Group Group { get; set; }

        [JsonPropertyName("network")]
        public Network Network { get; set; }
    }

    public class Group
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class Network
    {
        [JsonPropertyName("lora")]
        public Lora Lora { get; set; }
    }

    public class Lora
    {
        [JsonPropertyName("signalLevel")]
        public int SignalLevel { get; set; }

        [JsonPropertyName("rssi")]
        public int Rssi { get; set; }

        [JsonPropertyName("gatewayCnt")]
        public int GatewayCnt { get; set; }

        [JsonPropertyName("esp")]
        public double Esp { get; set; }

        [JsonPropertyName("sf")]
        public int Sf { get; set; }

        [JsonPropertyName("messageType")]
        public string MessageType { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("snr")]
        public int Snr { get; set; }

        [JsonPropertyName("ack")]
        public bool Ack { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("fcnt")]
        public int Fcnt { get; set; }

        [JsonPropertyName("devEUI")]
        public string DevEui { get; set; }
    }

    public class Value
    {
        [JsonPropertyName("payload")]
        public string Payload { get; set; }
    }
}
