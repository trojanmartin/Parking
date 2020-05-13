﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue) return dateTime; // do not modify "guard" values
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }
    }
}
