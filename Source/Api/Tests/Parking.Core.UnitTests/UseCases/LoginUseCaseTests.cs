using Moq;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.Gateways.Services;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.Models;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using Parking.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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


        [Fact]
        public async void LoginUseCase_UserDoesNotExist_UseCaseReturnsFalse()     
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.FindByName(It.IsAny<string>()))
                .Returns(Task.FromResult<User>(null));


            var useCase = new LoginUseCaseBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<LoginResponse>>().Object;

            var result = await useCase.HandleAsync(new LoginRequest("username", "password"), outputPort);

            Assert.False(result);
        }


        [Fact]
        public async void LoginUseCase_InvalidPassword_UseCaseReturnsFalse()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.FindByName(It.IsAny<string>()))
                .Returns(Task.FromResult(new User(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())));


            userRepo.Setup(x => x.CheckPassword(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns(Task
                    .FromResult(false));
          

            var useCase = new LoginUseCaseBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<LoginResponse>>();

            var result = await useCase.HandleAsync(new LoginRequest("username", "password"), outputPort.Object);         
            
            Assert.False(result);
        }


        [Fact]
        public async void LoginUseCase_ValidLogins_UseCaseReturnsTrue()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.FindByName(It.IsAny<string>()))
                .Returns(Task.FromResult(new User(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())));


            userRepo.Setup(x => x.CheckPassword(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns(Task
                    .FromResult(true));


            var jwtToken = new Mock<IJwtTokenFactory>();


            var token = new Token("id", "token", 55);

            jwtToken.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(token));


            var useCase = new LoginUseCaseBuilder()
            {
                UserRepository = userRepo.Object,
                JwtTokenFactory = jwtToken.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<LoginResponse>>();

            var result = await useCase.HandleAsync(new LoginRequest("username", "password"), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.IsAny<LoginResponse>()));          
                    
            Assert.True(result);
        }


        
    }


    internal class LoginUseCaseBuilder
    {
        public IUserRepository UserRepository { get; set; } = new Mock<IUserRepository>().Object;

        public IJwtTokenFactory JwtTokenFactory { get; set; } = new Mock<IJwtTokenFactory>().Object;


        public ILoginUseCase Build() => new LoginUseCase(UserRepository, JwtTokenFactory);
    }
}
