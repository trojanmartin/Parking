using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt
{
    public interface IExecutable
    {
        Task Execute();
    }
}
