using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt.Handlers
{
    public interface IMessageHandler
    {
        Task BeginListening();

        void StopListening();
    }
}
