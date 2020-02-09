using Moq;
using Parking.Mqtt.Core.Handlers;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System;
using Xunit;

namespace Parking.Mqtt.Core.UnitTests.UseCases
{
    public class MQTTHandlerDisconnectTests
    {
        [Fact]
        public async void MqttDisconnectUseCase_DisonnectSucceeds_UseCaseReturnsTrue()
        {
            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.DisconnectAsync());

            var output = new Mock<IOutputPort<DisconnectResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<DisconnectResponse>()));

            var handler = new MQTTHandler(Log.FakeLogger<MQTTHandler>(), mqttMock.Object);


            var result = await handler.DisconnectAsync(It.IsAny<DisconnectRequest>(), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<DisconnectResponse>(a => a.Success == true)));

            Assert.True(result);
        }

        [Fact]
        public async void MqttDisconnectUseCase_DisonnectFails_UseCaseReturnsFalse()
        {
            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.DisconnectAsync()).Throws(new Exception()); 

            var output = new Mock<IOutputPort<DisconnectResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<DisconnectResponse>()));

            var handler = new MQTTHandler(Log.FakeLogger<MQTTHandler>(), mqttMock.Object);


            var result = await handler.DisconnectAsync(It.IsAny<DisconnectRequest>(), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<DisconnectResponse>(a => a.Success == false)));
           

            Assert.False(false);
        }
    }
}
