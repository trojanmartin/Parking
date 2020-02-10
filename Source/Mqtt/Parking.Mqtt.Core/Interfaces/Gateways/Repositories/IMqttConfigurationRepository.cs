using Parking.Mqtt.Core.Models.MQTT;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Repositories
{
    public interface IMQTTConfigurationRepository
    {
        Task<IEnumerable<MQTTServerConfigurationDTO>> GetConfigurationsAsync();
        Task<MQTTServerConfigurationDTO> GetConfigurationAsync(int id);
        Task<bool> CreateConfigurationAsync(MQTTServerConfigurationDTO configuration);
        Task<bool> UpdateConfigurationAsync(MQTTServerConfigurationDTO configuration);
    }
}
