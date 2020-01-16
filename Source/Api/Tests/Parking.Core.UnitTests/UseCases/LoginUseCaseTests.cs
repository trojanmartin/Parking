using Moq;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.Gateways.Services;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using Parking.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Parking.Core.UnitTests.UseCases
{
    public class LoginUseCaseTests
    {
        [Fact]
        public async void LoginUseCase_UsernameOrPasswordIsEmpty_UseCaseReturnsTrue()     
        {


            var useCase = new LoginUseCaseBuilder().Build();

            var output = new Mock<IOutputPort<LoginResponse>>().Object;

            var result = await useCase.HandleAsync(new LoginRequest(string.Empty, ""), output);


            Assert.False(result);

        }



        
    }


    internal class LoginUseCaseBuilder
    {
        public IUserRepository UserRepository { get; set; } = new Mock<IUserRepository>().Object;

        public IJwtTokenFactory JwtTokenFactory { get; set; } = new Mock<IJwtTokenFactory>().Object;


        public ILoginUseCase Build() => new LoginUseCase(UserRepository, JwtTokenFactory);
    }
}
