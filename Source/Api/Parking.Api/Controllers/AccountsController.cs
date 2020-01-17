using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Presenters;
using Parking.Api.Routing;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.Models.UseCaseRequests;

namespace Parking.Api.Controllers
{
    
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

        [HttpPost]
        [Route(ApiRouting.Login)]
        public async Task<IActionResult> Login([FromBody] Models.Request.LoginRequest request)
        {
            await _loginUseCase.HandleAsync(new LoginRequest(request.UserName, request.Password), _loginPresenter);

            return _loginPresenter.Result;
        }

        [HttpPost]
        [Route(ApiRouting.Register)]
        public async Task<IActionResult> Register([FromBody] Models.Request.RegisterRequest request)
        {
            await _registerUseCase.HandleAsync(new RegisterRequest(request.Username, request.FirstName, request.LastName, request.Password, request.Email), _registerPresenter);

            return _registerPresenter.Result;
        }


    }
}