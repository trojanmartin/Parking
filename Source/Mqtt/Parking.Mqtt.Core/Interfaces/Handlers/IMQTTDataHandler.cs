using Parking.Mqtt.Core.Models.MQTT.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Interfaces.Handlers
{
    public interface IMQTTDataHandler : IDataHandler<MQTTMessageDTO>
    {
    }
}
