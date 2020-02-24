using Parking.Mqtt.Core.Models.MQTT.DataMessage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Repositories
{
    public interface IParkDataRepository
    {
        Task SaveAsync(IEnumerable<SensorData> data);
    }
}
