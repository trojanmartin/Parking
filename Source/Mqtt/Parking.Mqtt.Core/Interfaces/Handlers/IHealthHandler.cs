using Parking.Mqtt.Core.Interfaces.Base;
using Parking.Mqtt.Core.Models;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Handlers
{
    public interface IHealthHandler
    {
        Task<MQTTGetStatusGateResponse> GetHealthAsync();
    }
}
