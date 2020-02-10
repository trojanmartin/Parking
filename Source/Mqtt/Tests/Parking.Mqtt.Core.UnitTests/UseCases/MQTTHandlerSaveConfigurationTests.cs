using Microsoft.Extensions.Logging;
using Moq;
using Parking.Mqtt.Core.Handlers;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Threading.Tasks;
using Xunit;

namespace Parking.Mqtt.Core.UnitTests.UseCases
{
    public class MQTTHandlerSaveConfigurationTests
    {
        [Fact]
        public async void MQTTSaveConfiguration_SuccessfulySaved_ReturnsTrue()
        {
            var logger = new Mock<ILogger<MQTTHandler>>().Object;

            var mqttMock = new Mock<IMqttService>();
            var mqttRepo = new Mock<IMQTTConfigurationRepository>();

            mqttRepo.Setup(x => x.CreateConfigurationAsync(It.IsAny<MQTTServerConfiguration>()))
                    .Returns(Task.FromResult(true));

            var output = new Mock<IOutputPort<SaveConfigurationResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<SaveConfigurationResponse>()));

            var handler = new MQTTHandler(logger, mqttMock.Object, mqttRepo.Object) ;

            var result = await handler.SaveConfigurationAsync(new SaveConfigurationRequest(It.IsAny< MQTTServerConfiguration>()), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<SaveConfigurationResponse>(a => a.Success == true)));
            Assert.True(result);
        }
    }
}
