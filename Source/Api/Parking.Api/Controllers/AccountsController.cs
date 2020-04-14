using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Presenters;
using Parking.Api.Routing;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Handlers;
using Parking.Core.Models;
using Parking.Core.Models.Errors;
using Parking.Core.Models.UseCaseRequests;
using System.Threading.Tasks;

namespace Parking.Api.Controllers
{

    [Produces("application/json")]
    [ApiController]
    public class AccountsController : ControllerBase
    {        
        private readonly LoginPresenter _loginPresenter;       
        private readonly RegisterPresenter _registerPresenter;        
        private readonly GetUserPresenter _getUserPresenter;
        private readonly IAccountsHandler _accountsHandler;

        public AccountsController(LoginPresenter loginPresenter, RegisterPresenter registerPresenter, GetUserPresenter getUserPresenter, IAccountsHandler accountsHandler)
        {
            _loginPresenter = loginPresenter;
            _registerPresenter = registerPresenter;
            _getUserPresenter = getUserPresenter;
            _accountsHandler = accountsHandler;
        }


        /// <summary>
        /// Login to the application
        /// </summary> 
        /// <param name="request"></param>
        /// <returns>A newly created User with jwt token</returns>        
        [HttpPost]
        [Route(ApiRouting.Login)]
        [ProducesResponseType(typeof(Token),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] Models.Request.LoginRequest request)
        {
            await _accountsHandler.LogInAsync(new LoginRequestDTO(request.UserName, request.Password), _loginPresenter);

            return _loginPresenter.Result;
        }

        /// <summary>
        /// Register new user
        /// </summary>  
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRouting.Register)]
        [ProducesResponseType(typeof(BaseResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] Models.Request.RegisterRequest request)
        {
            await _accountsHandler.RegisterAsync(new RegisterRequestDTO(request.Username, request.FirstName, request.LastName, request.Password, request.Email), _registerPresenter);

            return _registerPresenter.Result;
        }


        /// <summary>
        /// Get user with given username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRouting.User)]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser(string username)
        {
            await _accountsHandler.GetUserAsync(username, _getUserPresenter);

            return _getUserPresenter.Result;
        }

        /// <summary>
        /// Only test method, checks if the user is authenticated.
        /// Send request to this method with Jwt token in header and  
        /// if you are authenticated, method returns text "This is private area"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("api/accounts/private")]
        public async Task<IActionResult> Private()
        {
            return Ok("This is private area");
        }


    }
}