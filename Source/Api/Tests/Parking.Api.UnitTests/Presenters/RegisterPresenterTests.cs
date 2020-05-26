using Moq;
using Parking.Api.Presenters;
using Parking.Core.Models;
using Parking.Core.Models.Errors;
using Parking.Core.Models.UseCaseResponses;
using System.Net;
using Xunit;

namespace Parking.Api.UnitTests.Presenters
{
    public class RegisterPresenterTests
    {
        [Fact]
        public void RegisterPresenter_UseCaseFails_ContainsBadRequest()
        {
            var presenter = new RegisterPresenter();

            presenter.CreateResponse(new RegisterResponseDTO(false,new ErrorResponse(new[] { new Error(It.IsAny<string>(), It.IsAny<string>()) })));

            Assert.Equal((int)HttpStatusCode.BadRequest, presenter.Result.StatusCode);
        }

        [Fact]
        public void RegisterPresenter_UseCaseSucceds_ContainsOk()
        { 
            var presenter = new RegisterPresenter();

            presenter.CreateResponse(new RegisterResponseDTO(true));

            Assert.Equal((int)HttpStatusCode.OK, presenter.Result.StatusCode);
        }
    }
}
