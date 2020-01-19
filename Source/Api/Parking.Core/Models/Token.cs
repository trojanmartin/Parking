using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Models
{
    public class Token
    {
        public string Id { get; }
        public string AuthToken { get; }
        public DateTime Expires{ get; }

        public Token(string id, string authToken, DateTime expires)
        {
            Id = id;
            AuthToken = authToken;
            Expires = expires;
        }
    }
}
