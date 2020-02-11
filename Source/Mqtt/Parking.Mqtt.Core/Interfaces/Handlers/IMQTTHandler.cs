

using Parking.Mqtt.Core.Models.MQTT.Requests;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Handlers
{
    public interface IMQTTHandler
    {
        Task<bool> ConnectAsync(ConnectRequest connectRequest, IOutputPort<ConnectResponse> outputPort);

        Task<bool> ConnectAsync(int configurationId, IOutputPort<ConnectResponse> outputPort);

        Task<bool> SubscribeAsync(SubscribeRequest subscribeRequest, IOutputPort<SubscribeResponse> outputPort);

        Task<bool> DisconnectAsync(DisconnectRequest disconnectRequest, IOutputPort<DisconnectResponse> outputPort);

        Task<bool> GetConfigurationAsync(GetConfigurationRequest configurationRequest, IOutputPort<GetConfigurationResponse> outputPort);

        Task<bool> SaveConfigurationAsync(SaveConfigurationRequest saveConfigurationRequest, IOutputPort<SaveConfigurationResponse> outputPort);

        Task<bool> UpdateConfigurationAsync(SaveConfigurationRequest saveConfigurationRequest, IOutputPort<SaveConfigurationResponse> outputPort);
    }
}
