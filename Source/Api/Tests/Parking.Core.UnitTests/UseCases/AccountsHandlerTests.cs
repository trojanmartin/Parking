using Moq;
using Parking.Core.Handlers;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.Gateways.Services;
using Parking.Core.Interfaces.Handlers;
using Parking.Core.Models;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Parking.Core.UnitTests.UseCases
{
    public class AccountsHandlerTests
    {
        [Fact]
        public async void Login_UsernameOrPasswordIsEmpty_ReturnsTrue()    
        {
            var handler = new AccountsHandlerBuilder().Build();

            var output = new Mock<IOutputPort<LoginResponseDTO>>().Object;

            var result = await handler.LogInAsync(new LoginRequestDTO(string.Empty, ""), output);

           

            Assert.False(result);
        }


        [Fact]
        public async void Login_UserDoesNotExist_ReturnsFalse()     
        {
            //Arrange
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<User>(null));


            var handler = new AccountsHandlerBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<LoginResponseDTO>>().Object;

            //Act
            var result = await handler.LogInAsync(new LoginRequestDTO("username", "password"), outputPort);


            //Assert
            Assert.False(result);
        }


        [Fact]
        public async void Login_InvalidPassword_ReturnsFalse()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())));


            userRepo.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns(Task
                    .FromResult(false));
          

            var handler = new AccountsHandlerBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<LoginResponseDTO>>();

            var result = await handler.LogInAsync(new LoginRequestDTO("username", "password"), outputPort.Object);         
            
            Assert.False(result);
        }


        [Fact]
        public async void Login_ValidLogins_ReturnsTrue()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())));


            userRepo.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns(Task
                    .FromResult(true));


            var handler = new AccountsHandlerBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<LoginResponseDTO>>();

            var result = await handler.LogInAsync(new LoginRequestDTO("username", "password"), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.IsAny<LoginResponseDTO>()));          
                    
            Assert.True(result);
        }

        [Fact]
        public async void Login_RepoThrowsUnexpectedException_ReturnTrue()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<User>()));

            userRepo.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                  .Throws(new Exception());


            var handler = new AccountsHandlerBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<LoginResponseDTO>>();

            var result = await handler.LogInAsync(It.IsAny<LoginRequestDTO>(), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.Is<LoginResponseDTO>(x => x.Success == false && x.ErrorResponse.Errors.Any())));
            Assert.False(result);
        }

        [Fact]
        public async void Register_RepoThrowsUnexpectedException_ReturnsFalse()
        {

            var userRepo = new Mock<IUserRepository>();

            userRepo.Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<string>()))
                    .Throws(new Exception());

            var handler = new AccountsHandlerBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();


            var outputPort = new Mock<IOutputPort<RegisterResponseDTO>>();

            var result = await handler.RegisterAsync(new RegisterRequestDTO(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.Is<RegisterResponseDTO>(x => x.Success == false && x.ErrorResponse.Errors.Any())));

            Assert.False(result);
        }

        [Fact]
        public async void Register_RepoCannotCreateUser_ReturnsFalse()
        {
            var userRepo = new Mock<IUserRepository>();

            userRepo.Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(false));

            var handler = new AccountsHandlerBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<RegisterResponseDTO>>();

            var result = await handler.RegisterAsync(new RegisterRequestDTO(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.Is<RegisterResponseDTO>(x => x.Success == false && x.ErrorResponse.Errors.Any())));
            Assert.False(result);
        }


        [Fact]
        public async void Register_UserCreated_ReturnsTrue()
        {
            var userRepo = new Mock<IUserRepository>();

            userRepo.Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(true));

            var handler = new AccountsHandlerBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<RegisterResponseDTO>>();

            var result = await handler.RegisterAsync(new RegisterRequestDTO(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), outputPort.Object);

            outputPort.Setup(x => x.CreateResponse(It.Is<RegisterResponseDTO>(x => x.Success == true && !x.ErrorResponse.Errors.Any())));
            Assert.True(result);
        }



    }


    internal class AccountsHandlerBuilder
    {
        public IUserRepository UserRepository { get; set; } = new Mock<IUserRepository>().Object;

        public IJwtTokenFactory JwtTokenFactory { get; set; } = new Mock<IJwtTokenFactory>().Object;


        public IAccountsHandler Build() => new AccountsHandler(UserRepository, JwtTokenFactory, Log.FakeLogger<AccountsHandler>());
    }
}
