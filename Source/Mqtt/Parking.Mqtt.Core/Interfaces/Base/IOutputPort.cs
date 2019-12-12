using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces
{
    public interface IOutputPort<TResponse>
    {
        void CreateResponse(TResponse response);
    }
}
