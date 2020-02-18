using Microsoft.Extensions.Logging;
using Moq;

namespace Parking.Mqtt.Api.UnitTests
{
    public static class Log
    {
        public static ILogger<T> FakeLogger<T>()
        {
            return new Mock<ILogger<T>>().Object;
        }
    }
}
