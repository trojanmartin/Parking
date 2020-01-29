using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Api.Routing
{
    public static class ApiRouting
    {
        public const string Login = "/api/accounts/login";
        public const string Register = "/api/accounts/register";
        public const string User = "/api/accounts/user/{username}";
        
    }
}
