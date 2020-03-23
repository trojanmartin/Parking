using Microsoft.Extensions.Logging;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using Parking.Mqtt.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Handlers
{
    public class MQTTDataHandler : IMQTTDataHandler
    {
        private const string _cachedSensorsList = "fiitList";
        private readonly ICacheService _cache;
        private readonly ILogger _logger;
        private readonly IParkDataRepository _parkDataRepo;
        public MQTTDataHandler(ILogger<MQTTDataHandler> logger, ICacheService cache,  IParkDataRepository parkDataRepo)
        {
            _logger = logger;
            _cache = cache;
            _parkDataRepo = parkDataRepo;
        }

        public async Task ProccesMessage(MQTTMessageDTO message)
        {
            await Task.Run(() =>
            {
                var data = Serializer.DeserializeToObject<RawSensorData>(message.Payload);

                var sensorId = data.Metadata.Network.Lora.DevEui;

                var cachedSensor = _cache.GetOrCreate<SensorData>(sensorId, () =>
                {                  

                    return new SensorData()
                    {
                        FCount = data.Metadata.Network.Lora.Fcnt,
                        Latutide = data.Location.Lat,
                        Longitude = data.Location.Lon,
                        ParkEntries = new List<ParkEntry>()
                    };
                });

                AddIdToWatchedEntries(sensorId);


                cachedSensor.ParkEntries.Add(new ParkEntry()
                {
                    TimeStamp = data.Timestamp,
                    //ak neparne, stojí tam auto
                    Parked = Convert.ToInt32(data.Value.Payload) % 2 == 1
                });
            });           

        }

        private void AddIdToWatchedEntries(string id)
        {
            var list = _cache.GetOrCreate(_cachedSensorsList, () => new List<string>());
            list.Add(id);
        }

        public async Task NormalizeFromCacheAndSaveToDBAsync()
        {
            var listOfSensors = await _cache.GetAsync<List<string>>(_cachedSensorsList);

            var toSave = new List<SensorData>();

            var actualTime = DateTimeOffset.Now;

            foreach(var id in listOfSensors)
            {
                var sensor = await _cache.GetAsync<SensorData>(id);

                var normalizedParkEntry = new SensorData()
                {
                    FCount = sensor.FCount,
                    Latutide = sensor.Latutide,
                    Longitude = sensor.Longitude,
                    ParkEntries = new List<ParkEntry>()
                    {
                        new ParkEntry()
                        {
                            Parked = sensor.ParkEntries.Any(x => x.Parked),
                            TimeStamp = actualTime
                        }
                    }
               };

                toSave.Add(normalizedParkEntry);
            }

            await _parkDataRepo.SaveAsync(toSave);
        }
    }
}
