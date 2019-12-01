using Parking.Communication.Mqtt.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt
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
