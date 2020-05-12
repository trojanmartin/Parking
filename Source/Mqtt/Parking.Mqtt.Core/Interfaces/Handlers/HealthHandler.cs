using Parking.Mqtt.Core.Interfaces.Base;
using Parking.Mqtt.Core.Models;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Handlers
{
    public interface HealthHandler
    {
        Task GetHealthAsync(IOutputPort<HealthDTO> outputPort);
    }
}
