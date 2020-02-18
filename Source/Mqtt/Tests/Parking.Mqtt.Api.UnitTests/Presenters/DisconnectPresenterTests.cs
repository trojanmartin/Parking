using Parking.Mqtt.Api.Presenters;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Net;
using Xunit;

namespace Parking.Mqtt.Api.UnitTests.Presenters
{
    public class DisconnectPresenterTests
    {
        [Fact]
        public void ContainsOk_WhenUseCase_Succeeds()
        {

            var response = new DisconnectResponse(true);

            var presenter = new DisconnectPresenter();

            presenter.CreateResponse(response);

            Assert.Equal((int)HttpStatusCode.OK, presenter.Result.StatusCode);
        }


        [Fact]
        public void ContainsIntervalServerError_WhenUseCase_Fails()
        {

            var response = new DisconnectResponse(false);

            var presenter = new DisconnectPresenter();

            presenter.CreateResponse(response);

            Assert.Equal((int)HttpStatusCode.InternalServerError, presenter.Result.StatusCode);
        }
    }
}
