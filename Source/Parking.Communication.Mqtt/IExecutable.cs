using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt.Library
{
    public interface IExecutable
    {
        Task ExecuteAsync();
    }
}
