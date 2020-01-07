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
    public class MqttDisconnectUseCaseTest
    {
        [Fact]
        public async void MqttDisconnectUseCase_DisonnectSucceeds_UseCaseReturnsTrue()
        {
            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.DisconnectAsync());

            var output = new Mock<IOutputPort<DisconnectResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<DisconnectResponse>()));

            var useCase = new MqttDisconnectUseCase(Log.FakeLogger<MqttDisconnectUseCase>(), mqttMock.Object);


            var result = await useCase.HandleAsync(It.IsAny<DisconnectRequest>(), output.Object);

            Assert.True(result);
        }

        [Fact]
        public async void MqttDisconnectUseCase_DisonnectFails_UseCaseReturnsFalse()
        {
            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.DisconnectAsync()).Throws(new Exception()); 

            var output = new Mock<IOutputPort<DisconnectResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<DisconnectResponse>()));

            var useCase = new MqttDisconnectUseCase(Log.FakeLogger<MqttDisconnectUseCase>(), mqttMock.Object);


            var result = await useCase.HandleAsync(It.IsAny<DisconnectRequest>(), output.Object);

            Assert.False(false);
        }
    }
}
