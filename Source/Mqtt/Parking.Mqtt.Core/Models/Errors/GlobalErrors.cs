﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Mqtt.Core.Models.Errors
{
    public static class GlobalErrors
    {
        public static Error UnexpectedError => new Error("Internal server error", "Unexpected error occurs while proccesing your request.");
    }
}
