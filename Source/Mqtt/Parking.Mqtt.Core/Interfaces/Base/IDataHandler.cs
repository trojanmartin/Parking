using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Handlers
{
    public interface IDataHandler<TMessage>
    {
        Task ProccesMessage(TMessage message);

        Task NormalizeFromCacheAndSaveToDBAsync();
    }
}
