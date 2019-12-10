using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces
{
    public interface IUseCaseRequestHandler<TRequest,TResponse> where TRequest : IRequest<TResponse>
    {
        Task<bool> HandleAsync(TRequest request, IOutputPort<TResponse> response);
    }
}
