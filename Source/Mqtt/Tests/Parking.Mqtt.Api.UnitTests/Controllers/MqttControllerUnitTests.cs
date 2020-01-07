using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Parking.Mqtt.Api.Controllers;
using Parking.Mqtt.Api.Models.Requests;
using Parking.Mqtt.Api.Presenters;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.Models.Gateways;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Core.UseCases;
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
            var logger = new Mock<ILogger<MqttListenUseCase>>().Object;

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.BeginListeningAsync(It.IsAny<IEnumerable<Parking.Mqtt.Core.Models.UseCaseRequests.Topic>>()))
                                                    .Returns(Task.FromResult(new MqttListenResponse(new List<string>() { "topic" }, true)));

            var controller = new FakeMqttController()
            {
                ListenUseCase = new MqttListenUseCase(logger,mqttMock.Object)
            }.Build();
            
            //act 
            var result = await controller.ListenAsync(new ListenApiRequest() { Topics = new List<Parking.Mqtt.Api.Models.Requests.Topic>() { new Models.Requests.Topic() { TopicName = "topic", QoS = MqttQualityOfService.AtLeastOnce } } });

            //assert
            var code = ((ContentResult)result).StatusCode;
            Assert.True(code.HasValue && code == (int)HttpStatusCode.OK);
        }
       

        [Fact]
        public async void Listen_ReturnsBadRequest_UseCase_Fails()
        {
            var logger = new Mock<ILogger<MqttListenUseCase>>().Object;

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.BeginListeningAsync(It.IsAny<IEnumerable<Parking.Mqtt.Core.Models.UseCaseRequests.Topic>>()))
                                                    .Returns(Task.FromResult(new MqttListenResponse(null, false)));

            var controller = new FakeMqttController()
            {
                ListenUseCase = new MqttListenUseCase(logger,mqttMock.Object)
            }.Build();

            var result = await controller.ListenAsync(new Models.Requests.ListenApiRequest() { Topics = new List<Parking.Mqtt.Api.Models.Requests.Topic>() { new Models.Requests.Topic() { TopicName = "topic", QoS = MqttQualityOfService.AtLeastOnce } } });

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Connnect_ReturnsOk_UseCase_Ok()
        {
            var logger = new Mock<ILogger<MqttConnectUseCase>>().Object;

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.ConnectAsync(It.IsAny<ConnectRequest>()))
                     .Returns(Task.FromResult(new MqttConnectResponse(true)));

            var controller = new FakeMqttController()
            {
                ConnectUseCase = new MqttConnectUseCase(logger,mqttMock.Object)
            }.Build();

            var result = await controller.ConnectAsync(new ConnectApiRequest());

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.OK);
        }

        [Fact]
        public async void Connnect_ReturnsBadRequest_UseCase_Fails()
        {
            var logger = new Mock<ILogger<MqttConnectUseCase>>().Object;

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.ConnectAsync(It.IsAny<ConnectRequest>()))
                     .Returns(Task.FromResult(new MqttConnectResponse(false)));

            var controller = new FakeMqttController()
            {
                ConnectUseCase = new MqttConnectUseCase(logger,mqttMock.Object)
            }.Build();

            var result = await controller.ConnectAsync(new ConnectApiRequest());

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Disconnect_ReturnsInternalServerError_UseCase_Fails()
        {

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.DisconnectAsync())
                    .Throws(new Exception());


            var controller = new FakeMqttController()
            {
                DisconnectUseCase = new MqttDisconnectUseCase(Log.FakeLogger<MqttDisconnectUseCase>(), mqttMock.Object)
            }.Build();

            var result = await controller.DisconnectAsync();


            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.InternalServerError);
        }


        [Fact]
        public async void Disconnect_ReturnsOk_UseCase_Succeeds()
        {

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(x => x.DisconnectAsync());                                

            var controller = new FakeMqttController()
            {
                DisconnectUseCase = new MqttDisconnectUseCase(Log.FakeLogger<MqttDisconnectUseCase>(), mqttMock.Object)
            }.Build();

            var result = await controller.DisconnectAsync();

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.OK);
        }

    }

    internal class FakeMqttController
    {
        public IListenUseCase ListenUseCase { get; set; } = new Mock<IListenUseCase>().Object;
        public ListenPresenter ListenPresenter { get; set; } = new Mock<ListenPresenter>().Object;

        public ILogger<MqttController> Logger { get; set; } = new Mock<ILogger<MqttController>>().Object;

        public IConnectUseCase ConnectUseCase { get; set; } = new Mock<IConnectUseCase>().Object;
        public ConnectPresenter ConnectPresenter { get; set; } = new Mock<ConnectPresenter>().Object;
        public IDisconnectUseCase DisconnectUseCase { get; set; } = new Mock<IDisconnectUseCase>().Object;
        public DisconnectPresenter DisconnectPresenter { get; set; } = new Mock<DisconnectPresenter>().Object;



        public MqttController Build()
        {
            return new MqttController(Logger,ListenUseCase, ListenPresenter, ConnectUseCase, ConnectPresenter, DisconnectUseCase, DisconnectPresenter);
        }
    }
}
