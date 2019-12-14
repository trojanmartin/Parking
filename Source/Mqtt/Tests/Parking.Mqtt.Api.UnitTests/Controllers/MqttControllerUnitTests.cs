using Parking.Mqtt.Core.Interfaces.UseCases;
using Parking.Mqtt.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using Parking.Mqtt.Core.Interfaces.Gateways.Services;
using Parking.Mqtt.Core.Models.Gateways.Services.Mqtt;
using System.Threading.Tasks;
using Parking.Mqtt.Core.Models.Gateways;
using Parking.Mqtt.Api.Controllers;
using Parking.Mqtt.Api.Presenters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Parking.Mqtt.Core.Models.UseCaseRequests;
using Parking.Mqtt.Api.Models.Requests;

namespace Parking.Mqtt.Api.UnitTests.Controllers
{
    
    public class MqttControllerUnitTests
    {
        [Fact]
        public async void Listen_ReturnsOk_UseCase_Suceeds()
        {
            //arange
            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.BeginListeningAsync(It.IsAny<IEnumerable<Parking.Mqtt.Core.Models.UseCaseRequests.Topic>>()))
                                                    .Returns(Task.FromResult(new MqttListenResponse(new List<string>() { "topic" }, true)));

            var controller = new FakeMqttController()
            {
                ListenUseCase = new MqttListenUseCase(mqttMock.Object)
            }.Build();
            
            //act 
            var result = await controller.ListenAsync(new Models.Requests.ListenApiRequest() { Topics = new List<Parking.Mqtt.Api.Models.Requests.Topic>() { new Models.Requests.Topic() { TopicName = "topic", QoS = MqttQualityOfService.AtLeastOnce } } });

            //assert
            var code = ((ContentResult)result).StatusCode;
            Assert.True(code.HasValue && code == (int)HttpStatusCode.OK);
        }
       

        [Fact]
        public async void Listen_ReturnsBadRequest_UseCase_Fails()
        {
            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.BeginListeningAsync(It.IsAny<IEnumerable<Parking.Mqtt.Core.Models.UseCaseRequests.Topic>>()))
                                                    .Returns(Task.FromResult(new MqttListenResponse(null, false)));

            var controller = new FakeMqttController()
            {
                ListenUseCase = new MqttListenUseCase(mqttMock.Object)
            }.Build();

            var result = await controller.ListenAsync(new Models.Requests.ListenApiRequest() { Topics = new List<Parking.Mqtt.Api.Models.Requests.Topic>() { new Models.Requests.Topic() { TopicName = "topic", QoS = MqttQualityOfService.AtLeastOnce } } });

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Connnect_ReturnsOk_UseCase_Ok()
        {

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.ConnectAsync(It.IsAny<ConnectRequest>()))
                     .Returns(Task.FromResult(new MqttConnectResponse(true)));

            var controller = new FakeMqttController()
            {
                ConnectUseCase = new MqttConnectUseCase(mqttMock.Object)
            }.Build();

            var result = await controller.ConnectAsync(new ConnectApiRequest());

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.OK);
        }

        [Fact]
        public async void Connnect_ReturnsBadReques_UseCase_Fails()
        {

            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.ConnectAsync(It.IsAny<ConnectRequest>()))
                     .Returns(Task.FromResult(new MqttConnectResponse(false)));

            var controller = new FakeMqttController()
            {
                ConnectUseCase = new MqttConnectUseCase(mqttMock.Object)
            }.Build();

            var result = await controller.ConnectAsync(new ConnectApiRequest());

            var resultCode = ((ContentResult)result).StatusCode;

            Assert.True(resultCode.HasValue && resultCode == (int)HttpStatusCode.BadRequest);
        }

    }

    internal class FakeMqttController
    {
        public IListenUseCase ListenUseCase { get; set; } = new Mock<IListenUseCase>().Object;
        public ListenPresenter ListenPresenter { get; set; } = new Mock<ListenPresenter>().Object;

        public IConnectUseCase ConnectUseCase { get; set; } = new Mock<IConnectUseCase>().Object;
        public ConnectPresenter ConnectPresenter { get; set; } = new Mock<ConnectPresenter>().Object;

        public MqttController Build()
        {
            return new MqttController(ListenUseCase, ListenPresenter, ConnectUseCase, ConnectPresenter);
        }
    }
}
