using Microsoft.Extensions.Logging;
using Parking.Mqtt.Core.Exceptions;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using Parking.Mqtt.Core.Models.MQTT.ParkingData;
using Parking.Mqtt.Core.Serialization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Handlers
{
    public class MQTTDataHandler : IMQTTDataHandler
    {
        private const string _cachedSensorsList = "FIIT";
        private const int _fiitParkingLotId = 1;
        private readonly ICacheService _cache;
        private readonly ILogger _logger;
        private readonly IParkingEntryRepository _parkEntryRepo;
        private readonly ISensorRepository _sensorRepo;
        private readonly IParkingSpotRepository _spotRepo;
        public MQTTDataHandler(ILogger<MQTTDataHandler> logger, ICacheService cache, IParkingEntryRepository parkEntryRepo, ISensorRepository sensorRepo, IParkingSpotRepository spotRepo)
        {
            _logger = logger;
            _cache = cache;
            _parkEntryRepo = parkEntryRepo;
            _sensorRepo = sensorRepo;
            _spotRepo = spotRepo;
        }

        public async Task ProccesMessage(MQTTMessageDTO message)
        {
            _logger.LogInformation("Received message. {@Message}", message);
            await Task.Run(() =>
            {
                var data = Serializer.DeserializeToObject<RawSensorData>(message.Payload);

                var sensorId = data.Deveui;

                if (sensorId == null || string.IsNullOrEmpty(sensorId))
                {
                    _logger.LogError("Message received with empty SensorId");
                    return;
                }

                var cachedSensor = _cache.GetOrCreate(sensorId, () =>
                {
                    AddIdToWatchedEntries(sensorId);
                    return new SensorData()
                    {
                        Devui = data.Deveui,
                        Name = data.Name,
                        ParkEntries = new ConcurrentBag<ParkEntry>()
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
            var list = _cache.GetOrCreate(_cachedSensorsList, () => new ConcurrentBag<string>());
            list.Add(id);
        }

        public async Task NormalizeFromCacheAndSaveToDBAsync()
        {

            _logger.LogInformation("Normalizing and saving data to database");

            try
            {
                var listOfSensors = await _cache.GetAsync<ConcurrentBag<string>>(_cachedSensorsList);

                if (listOfSensors == null)
                    return;

                var toSave = new List<SensorData>();

                var actualTime = DateTimeOffset.Now;

                foreach (var id in listOfSensors)
                {
                    var sensor = await _cache.GetAsync<SensorData>(id);

                    var normalizedParkEntry = new SensorData()
                    {
                        Devui = sensor.Devui,
                        Name = sensor.Name,
                        ParkEntries = sensor.ParkEntries.Count == 0 ? new ConcurrentBag<ParkEntry>() : new ConcurrentBag<ParkEntry>()
                        {
                            new ParkEntry()
                            {
                                Parked = sensor.ParkEntries.Any(x => x.Parked),
                                TimeStamp = actualTime                                
                            }
                        }
                    };

                    sensor.ParkEntries.Clear();
                    toSave.Add(normalizedParkEntry);
                }

              await SaveDataAsync(toSave);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while normalizing and saving data to database");
            }

        }


        private async Task SaveDataAsync(IEnumerable<SensorData> sensorsData)
        {
            foreach (var sensor in sensorsData)
            {

                //momentalne 1, je to Id parkoviska pri fiitke
                var spot = await _spotRepo.GetByIdAsync(sensor.Name, _fiitParkingLotId);

                if (spot == null)
                    await _spotRepo.InsertAsync(new ParkingSpot()
                    {
                        Name = sensor.Name,
                        ParkingLotId = _fiitParkingLotId
                    });

                spot = await _spotRepo.GetByIdAsync(sensor.Name, _fiitParkingLotId);

                var isNewSensor = await _sensorRepo.GetByIdAsync(sensor.Devui) == null ? true : false;

                if (isNewSensor)
                    await _sensorRepo.InsertAsync(new Sensor() 
                    { 
                        Active = true,
                        Devui = sensor.Devui,
                        ParkingSpotName = sensor.Name,
                        ParkingSpotParkingLotId = _fiitParkingLotId
                    });

                foreach (var parkEntry in sensor.ParkEntries)
                {
                    await _parkEntryRepo.InsertAsync(new ParkEntry()
                    {
                        Parked = parkEntry.Parked,
                        TimeStamp = parkEntry.TimeStamp,
                        ParkingSpotName = sensor.Name,
                        SensorDevui = sensor.Devui,
                        ParkingSpotParkingLotId = _fiitParkingLotId                        
                    });
                }
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
