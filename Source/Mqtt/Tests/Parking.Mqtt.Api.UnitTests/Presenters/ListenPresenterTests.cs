using Parking.Mqtt.Api.Presenters;
using Parking.Mqtt.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Parking.Mqtt.Api.UnitTests.Presenters
{
    public class ListenPresenterTests
    {
        [Fact]
        public void Contains_Ok_Status_Code_When_Use_Case_Succeeds()
        {
            var presenter = new ListenPresenter();

            presenter.CreateResponse(new ListenResponse(true));

            Assert.Equal((int)HttpStatusCode.OK, presenter.Result.StatusCode);
        }

        [Fact]
        public void Contains_BadRequest_Status_Code_UseCase_Fails()
        {
            var presenter = new ListenPresenter();

            presenter.CreateResponse(new ListenResponse(false));

            Assert.Equal((int)HttpStatusCode.BadRequest, presenter.Result.StatusCode);
        }
    }
}
