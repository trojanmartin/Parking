using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Parking.Mqtt.Api.Controllers;
using Parking.Mqtt.Api.Models.Requests;
using Parking.Mqtt.Api.Presenters;
using Parking.Mqtt.Core.Handlers;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.Handlers;
using Parking.Mqtt.Core.Models.Gateways;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.MQTT;
using Parking.Mqtt.Core.Models.MQTT.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Parking.Mqtt.Api.UnitTests.Controllers
{

    public class MqttControllerUnitTests
    {
        [Fact]
        public async void Listen_ReturnsOk_UseCase_Suceeds()
        {
            //arange
            var logger = new Mock<ILogger<MQTTHandler>>().Object;

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.SubscribeAsync(It.IsAny<IEnumerable<MQTTTServerConfiguration>>()))
                                                    .Returns(Task.FromResult(new MQTTSubscribeGateResponse(new List<MQTTTServerConfiguration>() { new MQTTTServerConfiguration("name", MQTTQualityOfService.AtLeastOnce)}, true)));

            var controller = new FakeMqttController()
            {
                MQTTHandler = new MQTTHandler(logger,mqttMock.Object)
            }.Build();
            
            //act 
            var result = await controller.SubscribeAsync(new ListenApiRequest() { Topics = new List<Parking.Mqtt.Api.Models.Requests.Topic>() { new Models.Requests.Topic() { TopicName = "topic", QoS = MQTTQualityOfService.AtLeastOnce } } });

            //assert
            var code = ((ContentResult)result).StatusCode;
            Assert.True(code.HasValue && code == (int)HttpStatusCode.OK);
        }
       

        [Fact]
        public async void Listen_ReturnsBadRequest_UseCase_Fails()
        {
            //arange
            var logger = new Mock<ILogger<MQTTHandler>>().Object;

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.SubscribeAsync(It.IsAny<IEnumerable<MQTTTServerConfiguration>>()))
                                                    .Returns(Task.FromResult(new MQTTSubscribeGateResponse(null, false)));

            var controller = new FakeMqttController()
            {
                MQTTHandler = new MQTTHandler(logger, mqttMock.Object)
            }.Build();

            var result = await controller.SubscribeAsync(new Models.Requests.ListenApiRequest() { Topics = new List<Parking.Mqtt.Api.Models.Requests.Topic>() { new Models.Requests.Topic() { TopicName = "topic", QoS = MQTTQualityOfService.AtLeastOnce } } });

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Connnect_ReturnsOk_UseCase_Ok()
        {
            var logger = new Mock<ILogger<MQTTHandler>>().Object;


            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.ConnectAsync(It.IsAny<MQTTServerConfiguration>()))
                     .Returns(Task.FromResult(new MQTTConnectGateResponse(true)));

            var controller = new FakeMqttController()
            {
                MQTTHandler = new MQTTHandler(logger, mqttMock.Object)
            }.Build();

            var result = await controller.ConnectAsync(new ConnectApiRequest());

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.OK);
        }

        [Fact]
        public async void Connnect_ReturnsBadRequest_UseCase_Fails()
        {
            var logger = new Mock<ILogger<MQTTHandler>>().Object;

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.ConnectAsync(It.IsAny<MQTTServerConfiguration>()))
                     .Returns(Task.FromResult(new MQTTConnectGateResponse(false)));

            var controller = new FakeMqttController()
            {
                MQTTHandler = new MQTTHandler(logger, mqttMock.Object)
            }.Build();

            var result = await controller.ConnectAsync(new ConnectApiRequest());

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Disconnect_ReturnsInternalServerError_UseCase_Fails()
        {
            var logger = new Mock<ILogger<MQTTHandler>>().Object;

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.DisconnectAsync())
                    .Throws(new Exception());


            var controller = new FakeMqttController()
            {
                MQTTHandler = new MQTTHandler(logger, mqttMock.Object)
            }.Build();

            var result = await controller.DisconnectAsync();


            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.InternalServerError);
        }


        [Fact]
        public async void Disconnect_ReturnsOk_UseCase_Succeeds()
        {
            var logger = new Mock<ILogger<MQTTHandler>>().Object;


            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.DisconnectAsync());                                

            var controller = new FakeMqttController()
            {
                MQTTHandler = new MQTTHandler(logger, mqttMock.Object)
            }.Build();

            var result = await controller.DisconnectAsync();

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.OK);
        }

    }

    internal class FakeMqttController
    {
        public IMQTTHandler MQTTHandler { get; set; } = new Mock<IMQTTHandler>().Object;
        public SubscribePresenter SubscribePresenter { get; set; } = new Mock<SubscribePresenter>().Object;
        public ILogger<MqttController> Logger { get; set; } = new Mock<ILogger<MqttController>>().Object;      
        public ConnectPresenter ConnectPresenter { get; set; } = new Mock<ConnectPresenter>().Object;    
        public DisconnectPresenter DisconnectPresenter { get; set; } = new Mock<DisconnectPresenter>().Object;   





        public MqttController Build()
        {
            return new MqttController(MQTTHandler, Logger, DisconnectPresenter, ConnectPresenter, SubscribePresenter);
        }
    }
}
