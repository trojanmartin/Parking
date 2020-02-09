using Parking.Mqtt.Core.Models.MQTT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Repositories
{
    public interface IMqttConfigurationRepository
    {
        Task<IEnumerable<MQTTServerConfiguration>> GetConfigurationsAsync();
        Task<MQTTServerConfiguration> GetConfigurationAsync(string id);
        Task<bool> CreateOrUpdateMqttStatusAsync(MQTTServerConfiguration configuration);
    }
}
