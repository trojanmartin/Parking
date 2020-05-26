namespace Parking.Mqtt.Core.Interfaces.Base
{
    public interface IOutputPort<TResponse>
    {
        void CreateResponse(TResponse response);
    }
}
