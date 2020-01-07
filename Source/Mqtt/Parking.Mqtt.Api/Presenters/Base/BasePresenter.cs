using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
