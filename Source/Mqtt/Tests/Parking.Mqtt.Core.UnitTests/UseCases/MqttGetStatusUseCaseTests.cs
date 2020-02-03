using Microsoft.Extensions.Logging;
using Moq;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using Parking.Mqtt.Core.UseCases;
using System;
using Xunit;

namespace Parking.Mqtt.Core.UnitTests.UseCases
{
    public class MqttGetStatusUseCaseTests
    {
        [Fact]
        public async void MqtGetStatusUseCase_Fails_ReturnsTrue()
        {
            var logger = new Mock<ILogger<MqttGetStatusUseCase>>().Object;

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.GetStatusAsync())
                    .Throws(It.IsAny<Exception>());

            var output = new Mock<IOutputPort<GetStatusResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<GetStatusResponse>()));

            var useCase = new MqttGetStatusUseCase(mqttMock.Object, logger);

            var result = await useCase.HandleAsync(new Models.UseCaseRequests.GetStatusRequest(), output.Object);            

            Assert.False(result);
        }
    }
}
