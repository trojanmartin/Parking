using Parking.Mqtt.Core.Models.MQTT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Repositories
{
    public interface IMQTTConfigurationRepository
    {
        Task<IEnumerable<MQTTServerConfiguration>> GetConfigurationsAsync();
        Task<MQTTServerConfiguration> GetConfigurationAsync(int id);
        Task<bool> CreateConfigurationAsync(MQTTServerConfiguration configuration);
        Task<bool> UpdateConfigurationAsync(MQTTServerConfiguration configuration);
    }
}
