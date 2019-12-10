using Parking.Mqtt.Core.Models.Gateways;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Services
{
    public interface IMqttService
    {
        Task<MqttListenResponse> BeginListeningAsync();
        Task<MqttListenResponse> StopListeningAsync();
    }
}
