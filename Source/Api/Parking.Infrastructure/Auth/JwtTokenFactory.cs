using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Parking.Core.Interfaces.Gateways.Services;
using Parking.Core.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Infrastructure.Auth
{
    public class JwtTokenFactory : IJwtTokenFactory
    {

        private readonly JwtTokenOptions _options;

        public JwtTokenFactory(IOptions<JwtTokenOptions> options)
        {
            _options = options.Value;
        }

        public async Task<Token> GenerateTokenAsync(string id, string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            };


            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
                );


            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMonths(_options.ValidTo)
                );

            var encodedToken =  new JwtSecurityTokenHandler().WriteToken(token);

            return new Token(encodedToken, DateTime.Now.AddMonths(_options.ValidTo));
        }
    }
}
