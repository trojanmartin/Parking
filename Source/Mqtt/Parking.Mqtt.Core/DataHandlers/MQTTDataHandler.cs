using Microsoft.Extensions.Logging;
using Parking.Mqtt.Core.Exceptions;
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

                var sensorId = data.Deveui;

                var cachedSensor = _cache.GetOrCreate<SensorData>(sensorId, () =>
                {
                    AddIdToWatchedEntries(sensorId);
                    return new SensorData()
                    {
                        Devui = data.Deveui,
                        Name = data.Name,
                        ParkEntries = new List<ParkEntry>()
                    };
                });

               


                cachedSensor.ParkEntries.Add(new ParkEntry()
                {
                    TimeStamp = UnixTimestampToDateTime(data.Timestamp),
                    //ak 1, stojí tam auto
                    Parked = data.Status == 1
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
            try
            {
                var listOfSensors = await _cache.GetAsync<List<string>>(_cachedSensorsList);

                var toSave = new List<SensorData>();

                var actualTime = DateTimeOffset.Now;

                foreach (var id in listOfSensors)
                {
                    var sensor = await _cache.GetAsync<SensorData>(id);

                    var normalizedParkEntry = new SensorData()
                    {
                        Devui = sensor.Devui,
                        Name = sensor.Name,
                        Position = sensor.Position,
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
            catch(NotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while normalizing and saving data to database");               
            }

        }

        private DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
        }
    }
}
