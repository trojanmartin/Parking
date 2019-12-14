using Moq;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using Parking.Mqtt.Core.UseCases;
using System.Threading.Tasks;
using Xunit;

namespace Parking.Mqtt.Core.UnitTests
{
    public class MqttConnectUseCaseTests
    {
        [Fact]
        public async void MqttConnectUseCase_SuccesfullyConnected_ReturnsTrue()
        {
            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.ConnectAsync(It.IsAny<ConnectRequest>()))
                    .Returns(Task.FromResult(new Models.Gateways.Services.Mqtt.MqttConnectResponse(true)));

            var output = new Mock<IOutputPort<ConnectResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<ConnectResponse>()));

            var useCase = new MqttConnectUseCase(mqttMock.Object);

            var result = await useCase.HandleAsync(new Models.UseCaseRequests.ConnectRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>()), output.Object);

            Assert.True(result);
        }

        [Fact]
        public async void MqttConnectUseCase_NotConnected_ReturnsFalse()
        {
            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.ConnectAsync(It.IsAny<ConnectRequest>()))
                    .Returns(Task.FromResult(new Models.Gateways.Services.Mqtt.MqttConnectResponse(false)));

            var output = new Mock<IOutputPort<ConnectResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<ConnectResponse>()));


            var useCase = new MqttConnectUseCase(mqttMock.Object);

            var result = await useCase.HandleAsync(new Models.UseCaseRequests.ConnectRequest("", "", 0, "", "", false, false, 0), output.Object);

            Assert.False(result);
        }

       
    }
}
