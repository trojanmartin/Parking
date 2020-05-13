using Parking.Mqtt.Core.Interfaces.Base;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core
{
    public class HealthHandler : IHealthHandler
    {

        private readonly IMqttService _client;

        public HealthHandler(IMqttService client)
        {
            this._client = client;
        }

        public async Task GetHealthAsync()
        {
            var response = await _client.GetStatusAsync();
        }
    }
}
