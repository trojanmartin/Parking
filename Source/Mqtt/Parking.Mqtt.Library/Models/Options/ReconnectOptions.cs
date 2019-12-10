using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Library.Models.Options
{
    public class ReconnectOptions
    {
        public bool Reconnect { get; set; }

        public TimeSpan After { get; set; }
    }
}
