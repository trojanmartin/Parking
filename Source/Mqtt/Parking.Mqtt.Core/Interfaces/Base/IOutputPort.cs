namespace Parking.Mqtt.Core.Interfaces
{
    public interface IOutputPort<TResponse>
    {
        void CreateResponse(TResponse response);
    }
}
