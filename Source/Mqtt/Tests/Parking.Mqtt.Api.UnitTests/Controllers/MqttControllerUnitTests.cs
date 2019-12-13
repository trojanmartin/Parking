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
using Parking.Mqtt.Service.Controllers;
using Parking.Mqtt.Api.Presenters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Parking.Mqtt.Core.Models.UseCaseRequests;

namespace Parking.Mqtt.Api.UnitTests.Controllers
{
    
    public class MqttControllerUnitTests
    {
        [Fact]
        public async void Listen_ReturnsOk_UseCase_Suceeds()
        {
            //arange
            var mqttMock = new Mock<IMqttService>();
            mqttMock.Setup(m => m.BeginListeningAsync(It.IsAny<IEnumerable<Topic>>()))
                                                    .Returns(Task.FromResult(new MqttListenResponse(new List<string>() { "topic" }, true)));

            var listeUseCase = new MqttListenUseCase(mqttMock.Object);
            var presenter = new ListenPresenter();

            var controller = new MqttController(listeUseCase, presenter, null, null);
            //act 
            var result = await controller.ListenAsync(new Models.Requests.ListenApiRequest() { Topics = new List<Parking.Mqtt.Api.Models.Requests.Topic>() { new Models.Requests.Topic() { TopicName = "topic", QoS = MqttQualityOfService.AtLeastOnce } } });

            var code = ((ContentResult)result).StatusCode;

            Assert.True(code.HasValue && code == (int)HttpStatusCode.OK);
        }
       

        [Fact]
        public async void Listen_ReturnsBadRequest_UseCase_Fails()
        {

        }
    }
}
