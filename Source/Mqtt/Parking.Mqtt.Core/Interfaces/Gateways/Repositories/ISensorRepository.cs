using Parking.Mqtt.Core.Interfaces.Base;
using Parking.Mqtt.Core.Models.MQTT.DataMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Interfaces.Gateways.Repositories
{
    public interface ISensorRepository : IBaseRepository<SensorData>
    {
    }
}
