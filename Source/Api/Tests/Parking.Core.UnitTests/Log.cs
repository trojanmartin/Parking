using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.UnitTests
{
    public static class Log
    {
        public static ILogger<T> FakeLogger<T>()
        {
            return new Mock<ILogger<T>>().Object;
        }
    }
}
