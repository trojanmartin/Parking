using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Infrastructure.Auth
{
    public class JwtTokenOptions
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string  Audience { get; set; }

        public int ValidTo { get; set; }
    }
}
