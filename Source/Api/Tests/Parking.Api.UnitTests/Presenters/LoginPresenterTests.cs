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
    public class LoginPresenterTests
    {
        [Fact]
        public void LoginPresenter_UseCaseSuceed_ContainsOkStatus()
        {
            var presenter = new LoginPresenter();

            presenter.CreateResponse(new LoginResponse(It.IsAny<User>(), It.IsAny<Token>(), true));

            Assert.Equal((int)HttpStatusCode.OK, presenter.Result.StatusCode);
        }

        [Fact]
        public void LoginPresenter_UseCaseFails_ContainsUnauthorized()
        {
            var presenter = new LoginPresenter();
            presenter.CreateResponse(new LoginResponse(new[] { new Error(It.IsAny<string>(), It.IsAny<string>())}, false));

            Assert.Equal((int)HttpStatusCode.Unauthorized, presenter.Result.StatusCode);
        }
    }
}
