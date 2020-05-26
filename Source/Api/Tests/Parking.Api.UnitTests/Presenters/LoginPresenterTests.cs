using Moq;
using Parking.Api.Presenters;
using Parking.Core.Models;
using Parking.Core.Models.Errors;
using Parking.Core.Models.UseCaseResponses;
using System.Net;
using Xunit;

namespace Parking.Api.UnitTests.Presenters
{
    public class LoginPresenterTests
    {
        [Fact]
        public void LoginPresenter_UseCaseSuceed_ContainsOkStatus()
        {
            var presenter = new LoginPresenter();

            presenter.CreateResponse(new LoginResponseDTO(It.IsAny<Token>(), true));

            Assert.Equal((int)HttpStatusCode.OK, presenter.Result.StatusCode);
        }

        [Fact]
        public void LoginPresenter_UseCaseFails_ContainsUnauthorized()
        {
            var presenter = new LoginPresenter();
            presenter.CreateResponse(new LoginResponseDTO(false,new ErrorResponse(new[] { new Error(It.IsAny<string>(), It.IsAny<string>())})));

            Assert.Equal((int)HttpStatusCode.Unauthorized, presenter.Result.StatusCode);
        }
    }
}
