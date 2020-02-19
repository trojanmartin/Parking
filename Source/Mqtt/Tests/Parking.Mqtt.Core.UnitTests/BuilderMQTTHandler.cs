using Microsoft.Extensions.Logging;
using Moq;
using Parking.Mqtt.Core.Handlers;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.Handlers;

namespace Parking.Mqtt.Core.UnitTests
{
    internal class BuilderMQTTHandler
    {
        public ILogger<MQTTHandler> Logger { get; set; } = new Mock<ILogger<MQTTHandler>>().Object;

        public IMQTTConfigurationRepository MQTTRepo { get; set; } = new Mock<IMQTTConfigurationRepository>().Object;

        public IMqttService MQTTService { get; set; } = new Mock<IMqttService>().Object;

        public IDataReceivedHandler DataReceivedHandler { get; set; } = new Mock<IDataReceivedHandler>().Object;

        public IMQTTHandler Build() => new MQTTHandler(Logger, MQTTService, MQTTRepo, DataReceivedHandler);
    }
}
