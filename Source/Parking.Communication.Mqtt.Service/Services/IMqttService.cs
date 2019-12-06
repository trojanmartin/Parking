using Parking.Communication.Mqtt.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Communication.Mqtt.Service.Services
{
    public interface IMqttService
    {
        Task<MqttResponse> BeginListeningAsync();                
        Task<MqttResponse> StopListeningAsync();                
    }
}
