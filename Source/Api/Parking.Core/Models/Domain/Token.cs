﻿using System;

namespace Parking.Core.Models
{
    public class Token
    {        
        public string AuthToken { get; }
        public DateTime Expires{ get; }

        public Token(string authToken, DateTime expires)
        {           
            AuthToken = authToken;
            Expires = expires;
        }
    }
}
