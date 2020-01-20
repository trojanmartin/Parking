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
using System.Linq;
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
            userRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
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
            userRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())));


            userRepo.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
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
            userRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())));


            userRepo.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns(Task
                    .FromResult(true));            


            var useCase = new LoginUseCaseBuilder()
            {
                UserRepository = userRepo.Object                
            }.Build();

            var outputPort = new Mock<IOutputPort<LoginResponse>>();

            var result = await useCase.HandleAsync(new LoginRequest("username", "password"), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.IsAny<LoginResponse>()));          
                    
            Assert.True(result);
        }

        [Fact]
        public async void LoginUseCase_RepoThrowsUnexpectedException_UseCaseReturnTrue()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<User>()));

            userRepo.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                  .Throws(new Exception());


            var useCase = new LoginUseCaseBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<LoginResponse>>();

            var result = await useCase.HandleAsync(It.IsAny<LoginRequest>(), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.Is<LoginResponse>(x => x.Success == false && x.Errors.Any())));
            Assert.False(result);
        }


        
    }


    internal class LoginUseCaseBuilder
    {
        public IUserRepository UserRepository { get; set; } = new Mock<IUserRepository>().Object;

        public IJwtTokenFactory JwtTokenFactory { get; set; } = new Mock<IJwtTokenFactory>().Object;


        public ILoginUseCase Build() => new LoginUseCase(Log.FakeLogger<LoginUseCase>(),UserRepository, JwtTokenFactory);
    }
}
