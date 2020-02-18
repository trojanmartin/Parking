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

            var mqttMock = new Mock<IMqttService>();

            mqttMock.Setup(x => x.SubscribeAsync(It.IsAny<IEnumerable<MQTTTopicConfigurationDTO>>()))
                    .Returns(Task.FromResult(new MQTTSubscribeGateResponse(It.IsAny<IEnumerable<MQTTTopicConfigurationDTO>>(), true)));


            var output = new Mock<IOutputPort<SubscribeResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<SubscribeResponse>()));

            var handler = new BuilderMQTTHandler()
            {
                MQTTService = mqttMock.Object
            }.Build();

            var result = await handler.SubscribeAsync(new SubscribeRequest(It.IsAny<IEnumerable<MQTTTopicConfigurationDTO>>()), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<SubscribeResponse>(a => a.Success)));
            Assert.True(result);
        }

        [Fact]
        public async void MQTTSubscribe_UnSuccessfulySubscribing_ReturnsFalse()
        {
           
            var mqttMock = new Mock<IMqttService>();

            mqttMock.Setup(x => x.SubscribeAsync(It.IsAny<IEnumerable<MQTTTopicConfigurationDTO>>()))
                  .Returns(Task.FromResult(new MQTTSubscribeGateResponse(It.IsAny<IEnumerable<MQTTTopicConfigurationDTO>>(), false)));


            var output = new Mock<IOutputPort<SubscribeResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<SubscribeResponse>()));

            var handler = new BuilderMQTTHandler()
            {
                MQTTService = mqttMock.Object
            }.Build();

            var result = await handler.SubscribeAsync(It.IsAny<SubscribeRequest>(), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<SubscribeResponse>(a => !a.Success)));

            Assert.False(result);
        }

        [Fact]
        public async void MQTTSubscribe_ServiceThrowsException_ReturnsFalse()
        {
           

            var mqttMock = new Mock<IMqttService>();

            mqttMock.Setup(x => x.SubscribeAsync(It.IsAny<IEnumerable<MQTTTopicConfigurationDTO>>()))
                    .Throws(new Exception());

            var output = new Mock<IOutputPort<SubscribeResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<SubscribeResponse>()));

            var handler = new BuilderMQTTHandler()
            {
                MQTTService = mqttMock.Object
            }.Build();

            var result = await handler.SubscribeAsync(It.IsAny<SubscribeRequest>(), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<SubscribeResponse>(a => !a.Success)));

            Assert.False(result);
        }
    }
}
