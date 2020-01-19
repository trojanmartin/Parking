using Microsoft.Extensions.Logging;
using Moq;
using Parking.Core.Interfaces.Base;
using Parking.Core.Interfaces.Gateways.Repositories;
using Parking.Core.Interfaces.UseCases;
using Parking.Core.Models;
using Parking.Core.Models.UseCaseRequests;
using Parking.Core.Models.UseCaseResponses;
using Parking.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using System.Threading.Tasks;
using Parking.Core.Models.Gateways.Repositories;

namespace Parking.Core.UnitTests.UseCases
{
    public class RegisterUseCaseTests
    {
        [Fact]
        public async void RegisterUserUseCase_RepoThrowsUnexpectedException_ReturnsFalse()
        {

            var userRepo = new Mock<IUserRepository>();

            userRepo.Setup(x => x.CreateUser(It.IsAny<User>(), It.IsAny<string>()))
                    .Throws(new Exception());

            var useCase = new RegisterUseCaseBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();


            var outputPort = new Mock<IOutputPort<RegisterResponse>>();         

            var result = await useCase.HandleAsync(new RegisterRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.Is<RegisterResponse>(x => x.Success == false && x.Errors.Any())));

            Assert.False(result);
        }


        [Fact]
        public async void RegisterUseCase_RepoCannotCreateUser_ReturnsFalse()
        {
            var userRepo = new Mock<IUserRepository>();

            userRepo.Setup(x => x.CreateUser(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(new CreateUserResponse(It.IsAny<string>(), false, new[] { new Error("Any error", "error description") })));

            var useCase = new RegisterUseCaseBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<RegisterResponse>>();

            var result = await useCase.HandleAsync(new RegisterRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), outputPort.Object);

            outputPort.Verify(x => x.CreateResponse(It.Is<RegisterResponse>(x => x.Success == false && x.Errors.Any())));
            Assert.False(result);
        }

        [Fact]
        public async void RegisterUserUseCase_UserCreated_ReturnsTrue()
        {
            var userRepo = new Mock<IUserRepository>();

            userRepo.Setup(x => x.CreateUser(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(new CreateUserResponse(It.IsAny<string>(), true)));

            var useCase = new RegisterUseCaseBuilder()
            {
                UserRepository = userRepo.Object
            }.Build();

            var outputPort = new Mock<IOutputPort<RegisterResponse>>();

            var result = await useCase.HandleAsync(new RegisterRequest(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), outputPort.Object);

            outputPort.Setup(x => x.CreateResponse(It.Is<RegisterResponse>(x => x.Success == true && !x.Errors.Any())));
            Assert.True(result);
        }
    }


    internal class RegisterUseCaseBuilder
    {

        public IUserRepository UserRepository { get; set; } = new Mock<IUserRepository>().Object;


        public RegisterUserUseCase Build() => new RegisterUserUseCase(Log.FakeLogger<RegisterUserUseCase>(), UserRepository);
    }
}
