namespace Parking.Mqtt.Api.Presenters.Base
{
    public class BasePresenter
    {
        public JsonContentResult Result { get; }

        public BasePresenter()
        {
            Result = new JsonContentResult();
        }
    }
}
