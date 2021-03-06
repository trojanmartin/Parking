﻿using Parking.Mqtt.Core.Models.Errors;

namespace Parking.Mqtt.Core.Models
{
    public class Error
    {
        public string Code { get; }

        public string  Description { get; }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
