using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Base
{
    public interface IListener<TMessage>
    {
        Task HandleAsync(TMessage message, IOutputPort<TMessage> response = null); 
    }
}
