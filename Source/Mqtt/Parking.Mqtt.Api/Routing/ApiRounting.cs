using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Mqtt.Api.Routing
{
    public static class ApiRouting
    {
        public const string Listen = "/api/mqtt/listen";
        public const string Connect = "/api/mqtt/connect";
        public const string Disconnect = "/api/mqtt/disconnect";       
    }
}
