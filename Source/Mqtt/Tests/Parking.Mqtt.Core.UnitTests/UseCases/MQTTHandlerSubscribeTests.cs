using Microsoft.Extensions.Logging;
using Moq;
using Parking.Mqtt.Core.Handlers;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.Gateways;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Parking.Mqtt.Core.UnitTests.UseCases
{
    public class MQTTHandlerSubscribeTests
    {
        [Fact]
        public async void MQTTSubscribe_SuccessfulySubscribing_ReturnsTrue()
        {
            var logger = new Mock<ILogger<MQTTHandler>>().Object;

            var mqttMock = new Mock<IMqttService>();

            mqttMock.Setup(x => x.SubscribeAsync(It.IsAny<IEnumerable<MQTTTopicConfiguration>>()))
                    .Returns(Task.FromResult(new MQTTSubscribeGateResponse(It.IsAny<IEnumerable<MQTTTopicConfiguration>>(), true)));


            var output = new Mock<IOutputPort<SubscribeResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<SubscribeResponse>()));

            var handler = new MQTTHandler(logger,mqttMock.Object);

            var result = await handler.SubscribeAsync(new SubscribeRequest(It.IsAny<IEnumerable<MQTTTopicConfiguration>>()), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<SubscribeResponse>(a => a.Success == true)));
            Assert.True(result);
        }

        [Fact]
        public async void MQTTSubscribe_UnSuccessfulySubscribing_ReturnsFalse()
        {
            var logger = new Mock<ILogger<MQTTHandler>>().Object;

            var mqttMock = new Mock<IMqttService>();

            mqttMock.Setup(x => x.SubscribeAsync(It.IsAny<IEnumerable<MQTTTopicConfiguration>>()))
                  .Returns(Task.FromResult(new MQTTSubscribeGateResponse(It.IsAny<IEnumerable<MQTTTopicConfiguration>>(), false)));


            var output = new Mock<IOutputPort<SubscribeResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<SubscribeResponse>()));

            var handler = new MQTTHandler(logger, mqttMock.Object);

            var result = await handler.SubscribeAsync(It.IsAny<SubscribeRequest>(), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<SubscribeResponse>(a => a.Success == false)));

            Assert.False(result);
        }

        [Fact]
        public async void MQTTSubscribe_ServiceThrowsException_ReturnsFalse()
        {
            var logger = new Mock<ILogger<MQTTHandler>>().Object;

            var mqttMock = new Mock<IMqttService>();

            mqttMock.Setup(x => x.SubscribeAsync(It.IsAny<IEnumerable<MQTTTopicConfiguration>>()))
                    .Throws(new Exception());

            var output = new Mock<IOutputPort<SubscribeResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<SubscribeResponse>()));

            var handler = new MQTTHandler(logger, mqttMock.Object);

            var result = await handler.SubscribeAsync(It.IsAny<SubscribeRequest>(), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<SubscribeResponse>(a => a.Success == false)));

            Assert.False(result);
        }
    }
}
