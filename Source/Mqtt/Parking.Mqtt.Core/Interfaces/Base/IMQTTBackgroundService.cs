using System.Threading.Tasks;

namespace Parking.Mqtt.Core.Interfaces.Base
{
    public interface IMQTTBackgroundService
    {
        Task Configure();
    }
}
