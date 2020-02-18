namespace Parking.Mqtt.Core.Interfaces.Base
{
    public abstract class BaseRepositoryResponse
    {
        protected BaseRepositoryResponse(int id)
        {
            Id = id;
        }

        public int Id{ get; }
    }
}
