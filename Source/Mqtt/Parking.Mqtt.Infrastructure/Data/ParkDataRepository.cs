using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Infrastructure.Data
{
    public class ParkDataRepository : IParkDataRepository
    {
        public Task SaveAsync(IEnumerable<SensorData> data)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
