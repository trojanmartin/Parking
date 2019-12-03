using Parking.Communication.Mqtt.Library.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt.Library
{
    public class Application : IExecutable
    {
        private readonly IMessageHandler _messageHandler;

        public Application(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public async Task Execute()
        {
            await _messageHandler.BeginListening();
        }
    }
}
