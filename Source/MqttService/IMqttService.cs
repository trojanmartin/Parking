using MqttService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MqttService
{
    public interface IMqttService
    {
        Task Connect(MqttOptions options);

    }
}
