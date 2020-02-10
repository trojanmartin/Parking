﻿using Moq;
using Parking.Mqtt.Core.Interfaces;
using Parking.Mqtt.Core.Interfaces.Gateways.Repositories;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.Requests;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using System;

namespace Parking.Mqtt.Core.UnitTests.UseCases
{
    public class MQTTHandlerGetConfigurationTests
    {
        [Fact]
        public async void GetConfiguration_GetOneConfigurationWithId_ReturnsTrue()
        {
            var mqttRepo = new Mock<IMQTTConfigurationRepository>();

            mqttRepo.Setup(x => x.GetConfigurationAsync(It.IsAny<int>()))
                    .Returns(Task.FromResult(It.IsAny<MQTTServerConfigurationDTO>()));

            var handler = new BuilderMQTTHandler()
            {
                MQTTRepo = mqttRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<GetConfigurationResponse>>();
            outputPort.Setup(x => x.CreateResponse(It.IsAny<GetConfigurationResponse>()));

            var result = await handler.GetConfigurationAsync(new GetConfigurationRequest(It.IsAny<int>()), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.Is<GetConfigurationResponse>(x => x.Success)));
            Assert.True(result);

            
        }

        [Fact]
        public async void GetConfiguration_GetAllConfigurations_ReturnsTrue()
        {
            var mqttRepo = new Mock<IMQTTConfigurationRepository>();

            mqttRepo.Setup(x => x.GetConfigurationsAsync())
                    .Returns(Task.FromResult(It.IsAny<IEnumerable<MQTTServerConfigurationDTO>>()));

            var handler = new BuilderMQTTHandler()
            {
                MQTTRepo = mqttRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<GetConfigurationResponse>>();
            outputPort.Setup(x => x.CreateResponse(It.IsAny<GetConfigurationResponse>()));

            var result = await handler.GetConfigurationAsync(new GetConfigurationRequest(null), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.Is<GetConfigurationResponse>(x => x.Success)));
            Assert.True(result);
        }

        [Fact]
        public async void GetConfiguration_GetAllConfigurationsFails_ReturnsFalse()
        {
            var mqttRepo = new Mock<IMQTTConfigurationRepository>();

            mqttRepo.Setup(x => x.GetConfigurationsAsync())
                    .Throws(It.IsAny<Exception>());

            var handler = new BuilderMQTTHandler()
            {
                MQTTRepo = mqttRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<GetConfigurationResponse>>();
            outputPort.Setup(x => x.CreateResponse(It.IsAny<GetConfigurationResponse>()));

            var result = await handler.GetConfigurationAsync(new GetConfigurationRequest(null), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.Is<GetConfigurationResponse>(x => !x.Success)));
            Assert.False(result);
        }
    }
}
