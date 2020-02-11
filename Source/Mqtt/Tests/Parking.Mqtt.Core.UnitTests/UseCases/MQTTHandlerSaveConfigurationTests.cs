using Moq;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Parking.Mqtt.Core.UnitTests.UseCases
{
    public class MQTTHandlerSaveConfigurationTests
    {
        [Fact]
        public async void MQTTSaveConfiguration_SuccessfulySaved_ReturnsTrue()
        {                      
            var mqttRepo = new Mock<IMQTTConfigurationRepository>();

            mqttRepo.Setup(x => x.CreateConfigurationAsync(It.IsAny<MQTTServerConfigurationDTO>()))
                    .Returns(Task.FromResult(true));

            var output = new Mock<IOutputPort<SaveConfigurationResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<SaveConfigurationResponse>()));

            var handler = new BuilderMQTTHandler()
            {               
                MQTTRepo = mqttRepo.Object

            }.Build();

            var result = await handler.SaveConfigurationAsync(new SaveConfigurationRequest(It.IsAny< MQTTServerConfigurationDTO>()), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<SaveConfigurationResponse>(a => a.Success)));
            Assert.True(result);
        }


        [Fact]
        public async void MQTTSaveConfiguration_RepoThrowsException_ReturnsFalse()
        {
            var mqttRepo = new Mock<IMQTTConfigurationRepository>();

            mqttRepo.Setup(x => x.CreateConfigurationAsync(It.IsAny<MQTTServerConfigurationDTO>()))
                    .Throws(new Exception());

            var output = new Mock<IOutputPort<SaveConfigurationResponse>>();
            output.Setup(x => x.CreateResponse(It.IsAny<SaveConfigurationResponse>()));

            var handler = new BuilderMQTTHandler()
            {
                MQTTRepo = mqttRepo.Object
            }.Build();

            var result = await handler.SaveConfigurationAsync(new SaveConfigurationRequest(It.IsAny<MQTTServerConfigurationDTO>()), output.Object);

            output.Verify(x => x.CreateResponse(It.Is<SaveConfigurationResponse>(a => !a.Success)));
            Assert.False(result);
        }
    }
}
