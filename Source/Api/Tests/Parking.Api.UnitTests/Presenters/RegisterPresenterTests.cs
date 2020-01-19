using Moq;
using Parking.Api.Presenters;
using Parking.Core.Models;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Parking.Api.UnitTests.Presenters
{
    public class RegisterPresenterTests
    {
        [Fact]
        public void RegisterPresenter_UseCaseFails_ContainsBadRequest()
        {
            var presenter = new RegisterPresenter();

            presenter.CreateResponse(new RegisterResponse(new[] { new Error(It.IsAny<string>(), It.IsAny<string>()) }, false));

            Assert.Equal((int)HttpStatusCode.BadRequest, presenter.Result.StatusCode);
        }

        [Fact]
        public void RegisterPresenter_UseCaseSucceds_ContainsOk()
        { 
            var presenter = new RegisterPresenter();

            presenter.CreateResponse(new RegisterResponse(It.IsAny<string>(), true));

            Assert.Equal((int)HttpStatusCode.OK, presenter.Result.StatusCode);
        }
    }
}
