using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Base
{
    public interface IUseCaseRequestHandler<TRequest,TResponse> where TRequest : IRequest<TResponse>
    {
        Task<bool> HandleAsync(TRequest request, IOutputPort<TResponse> response);
    }
}
