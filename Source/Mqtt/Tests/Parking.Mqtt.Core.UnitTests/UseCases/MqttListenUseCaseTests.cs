using Microsoft.Extensions.Logging;
using Moq;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using Parking.Mqtt.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Parking.Mqtt.Core.UnitTests.UseCases
{
    public class MqttListenUseCaseTests
    {
        [Fact]
        public async void MqttListen_SuccessfulyListening_ReturnsTrue()
        {
            var logger = new Mock<ILogger<MqttListenUseCase>>().Object;

            var mqttMock = new Mock<IMqttService>();

            mqttMock.Setup(x => x.BeginListeningAsync(It.IsAny<IEnumerable<Topic>>()))
                    .Returns(Task.FromResult(new Models.Gateways.MqttListenResponse(It.IsAny<IEnumerable<string>>(), true)));


            var output = new Mock<IOutputPort<ListenResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<ListenResponse>()));

            var useCase = new MqttListenUseCase(logger,mqttMock.Object);

            var result = await useCase.HandleAsync(new ListenRequest(It.IsAny<IEnumerable<Topic>>()), output.Object);

            Assert.True(result);
        }

        [Fact]
        public async void MqttListen_UnSuccessfulyListening_ReturnsFalse()
        {
            var logger = new Mock<ILogger<MqttListenUseCase>>().Object;

            var mqttMock = new Mock<IMqttService>();

            mqttMock.Setup(x => x.BeginListeningAsync(It.IsAny<IEnumerable<Topic>>()))
                    .Returns(Task.FromResult(new Models.Gateways.MqttListenResponse(It.IsAny<IEnumerable<string>>(), false)));


            var output = new Mock<IOutputPort<ListenResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<ListenResponse>()));

            var useCase = new MqttListenUseCase(logger,mqttMock.Object);

            var result = await useCase.HandleAsync(new ListenRequest(It.IsAny<IEnumerable<Topic>>()), output.Object);

            Assert.False(result);
        }

        [Fact]
        public async void MqttListen_ServiceThrowsException_ReturnsFalse()
        {
            var mqttMock = new Mock<IMqttService>();

            mqttMock.Setup(x => x.BeginListeningAsync(It.IsAny<IEnumerable<Topic>>()))
                    .Throws(new Exception());

            var output = new Mock<IOutputPort<ListenResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<ListenResponse>()));

            var useCase = new MqttListenUseCase(Log.FakeLogger<MqttListenUseCase>(), mqttMock.Object);

            var result = await useCase.HandleAsync(new ListenRequest(It.IsAny<IEnumerable<Topic>>()), output.Object);

            Assert.False(result);
        }
    }
}
