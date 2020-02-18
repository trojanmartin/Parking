using Parking.Mqtt.Api.Presenters;
using Parking.Mqtt.Core.Models.MQTT.Responses;
using System.Net;
using Xunit;

namespace Parking.Mqtt.Api.UnitTests.Presenters
{
    public class SubscribePresenterTests
    {
        [Fact]
        public void Contains_Ok_Status_Code_When_Use_Case_Succeeds()
        {
            var presenter = new SubscribePresenter();

            presenter.CreateResponse(new SubscribeResponse(true));

            Assert.Equal((int)HttpStatusCode.OK, presenter.Result.StatusCode);
        }

        [Fact]
        public void Contains_BadRequest_Status_Code_UseCase_Fails()
        {
            var presenter = new SubscribePresenter();

            presenter.CreateResponse(new SubscribeResponse(false));

            Assert.Equal((int)HttpStatusCode.BadRequest, presenter.Result.StatusCode);
        }
    }
}
