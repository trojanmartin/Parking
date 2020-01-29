using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Base
{
    public interface IParametrizedUseCaseHandler<TResponse> 
    {
        Task<bool> HandleAsync(string param, IOutputPort<TResponse> response);
    }
}
