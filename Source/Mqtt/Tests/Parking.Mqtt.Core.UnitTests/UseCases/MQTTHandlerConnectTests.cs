using Microsoft.Extensions.Logging;
using Moq;
using Parking.Mqtt.Core.Handlers;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Parking.Mqtt.Core.UnitTests
{
    public class MQTTHandlerConnectTests
    {
        [Fact]
        public async void MqttConnectUseCase_SuccesfullyConnected_ReturnsTrue()
        {          

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.ConnectAsync(It.IsAny<MQTTServerConfigurationDTO>()))
                    .Returns(Task.FromResult(new MQTTConnectGateResponse(true)));

            var output = new Mock<IOutputPort<ConnectResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<ConnectResponse>()));

            var handler = new BuilderMQTTHandler()
            {
                MQTTService = mqttMock.Object
            }.Build();

            var result = await handler.ConnectAsync(new ConnectRequest(It.IsAny<MQTTServerConfigurationDTO>()), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<ConnectResponse>(a => a.Success == true)));
            Assert.True(result);
        }

        [Fact]
        public async void MqttConnectUseCase_NotConnected_ReturnsFalse()
        {         

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.ConnectAsync(It.IsAny<MQTTServerConfigurationDTO>()))
                    .Returns(Task.FromResult(new MQTTConnectGateResponse(false)));

            var output = new Mock<IOutputPort<ConnectResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<ConnectResponse>()));


            var handler = new BuilderMQTTHandler()
            {
                MQTTService = mqttMock.Object
            }.Build();

            var result = await handler.ConnectAsync(It.IsAny<ConnectRequest>(), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<ConnectResponse>(a => a.Success == false)));

            Assert.False(result);
        }

        [Fact]
        public async void MqttConnectUseCase_ServiceThrowsExceptions_ReturnsFalse()
        {           

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.ConnectAsync(It.IsAny<MQTTServerConfigurationDTO>()))
                    .Throws(new Exception());
                  

            var output = new Mock<IOutputPort<ConnectResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<ConnectResponse>()));

            var handler = new BuilderMQTTHandler()
            {
                MQTTService = mqttMock.Object
            }.Build();

            var result = await handler.ConnectAsync(It.IsAny<ConnectRequest>(), output.Object);


            output.Verify(x => x.CreateResponse(It.Is<ConnectResponse>(a => a.Success == false)));
            Assert.False(result);
        }

       
    }
}