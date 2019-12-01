using System;
using System.Runtime.Serialization;

namespace Parking.Communication.Mqtt
{
    [Serializable]
    internal class ReconnectFailedException : Exception
    {

        public ReconnectFailedException()
        {
        }

        public ReconnectFailedException(string message) : base(message)
        {
        }

   

        public ReconnectFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReconnectFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}