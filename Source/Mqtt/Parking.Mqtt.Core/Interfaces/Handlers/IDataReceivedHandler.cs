using Parking.Mqtt.Core.Models.MQTT.DTO;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Handlers
{
    public interface IDataReceivedHandler
    {
        Task ProccesMQTTMessage(MQTTMessageDTO message);

        Task NormalizeFromCacheAndSaveToDBAsync();
    }
}
