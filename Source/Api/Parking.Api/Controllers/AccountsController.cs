using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Presenters;
using Parking.Api.Routing;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.Models;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using Swashbuckle.Swagger.Annotations;

namespace Parking.Api.Controllers
{

    [Produces("application/json")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILoginUseCase _loginUseCase;
        private readonly LoginPresenter _loginPresenter;

        private readonly IRegisterUseCase _registerUseCase;
        private readonly RegisterPresenter _registerPresenter;

        public AccountsController(ILoginUseCase loginUseCase, LoginPresenter loginPresenter, IRegisterUseCase registerUseCase, RegisterPresenter registerPresenter)
        {
            _loginUseCase = loginUseCase;
            _loginPresenter = loginPresenter;
            _registerUseCase = registerUseCase;
            _registerPresenter = registerPresenter;
        }


        /// <summary>
        /// Login to the application
        /// </summary> 
        /// <param name="request"></param>
        /// <returns>A newly created User with jwt token</returns>        
        [HttpPost]
        [Route(ApiRouting.Login)]
        [ProducesResponseType(typeof(LoginResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] Models.Request.LoginRequest request)
        {
            await _loginUseCase.HandleAsync(new LoginRequest(request.UserName, request.Password), _loginPresenter);

            return _loginPresenter.Result;
        }

        /// <summary>
        /// Register new user
        /// </summary>  
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRouting.Register)]
        [ProducesResponseType( typeof(RegisterResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterResponse),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] Models.Request.RegisterRequest request)
        {
            await _registerUseCase.HandleAsync(new RegisterRequest(request.Username, request.FirstName, request.LastName, request.Password, request.Email), _registerPresenter);

            return _registerPresenter.Result;
        }

        [HttpPost]
        [Authorize]
        [Route("api/accounts/private")]
        public async Task<IActionResult> Private()
        {
            return new JsonResult("This is private area");
        }


    }
}