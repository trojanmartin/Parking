using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces
{
    public interface IOutputPort<TResponse>
    {
        Task Handle(TResponse response);
    }
}
