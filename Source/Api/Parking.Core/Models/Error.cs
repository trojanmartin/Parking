using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models
{
    public class Error
    {
        public string Code { get; }

        public string Description { get; }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
