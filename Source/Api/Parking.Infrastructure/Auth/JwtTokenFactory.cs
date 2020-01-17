using Parking.Core.Interfaces.Gateways.Services;
using Parking.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Infrastructure.Auth
{
    public class JwtTokenFactory : IJwtTokenFactory
    {
        public Task<Token> GenerateToken(string id, string username)
        {
            throw new NotImplementedException();
        }
    }
}
