using Moq;
using Parking.Api.Presenters;
using Parking.Core.Models;
using Parking.Core.Models.Errors;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Parking.Api.UnitTests.Presenters
{
    public class StandardResponsePresenterTests
    {
        [Fact]
        public void StandardResponsePresenter_UseCaseFails_ContainsInternalServerError()
        {
            var presenter = new StandardResponsePresenter();

            presenter.CreateResponse(new StandardResponse(false, new ErrorResponse(new[] { new Error(It.IsAny<string>(), It.IsAny<string>()) })));

            Assert.Equal((int)HttpStatusCode.InternalServerError, presenter.Result.StatusCode);
        }


        [Fact]
        public void StandardResponsePresenter_UseCaseOk_ContainsOK()
        {
            var presenter = new StandardResponsePresenter();

            presenter.CreateResponse(new StandardResponse(true,It.IsAny<string>()));

            Assert.Equal((int)HttpStatusCode.OK, presenter.Result.StatusCode);
        }
    }
}