using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Models.Mqtt.Options
{
    public class ReconnectOptions
    {
        public bool Reconnect { get; set; }

        public TimeSpan After { get; set; }
    }
}
