using Parking.Mqtt.Core.Interfaces.Base;
using Parking.Mqtt.Core.Models.Gateways.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Interfaces.UseCases
{
    public interface IMessageListener : IListener<MqttMessage>
    {
    }
}
