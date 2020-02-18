using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Base
{
    public interface IOutputPort<TResponse>
    {
        void CreateResponse(TResponse response);
    }
}
