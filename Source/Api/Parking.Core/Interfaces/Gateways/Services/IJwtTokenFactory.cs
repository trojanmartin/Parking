using Parking.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Interfaces.Gateways.Services
{
    public interface IJwtTokenFactory
    {
        Task<Token> GenerateToken(string id, string username);
    }
}
