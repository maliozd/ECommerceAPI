using ECommerceAPI.Application.Abstraction.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration configuration;
        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Application.Dtos.Token CreateJwtToken(int expirationMinute)
        {
            Application.Dtos.Token token = new()
            {
                Expiration = DateTime.UtcNow.AddMinutes(expirationMinute)
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256); //şifrelenmiş kimlik

            token.Expiration = DateTime.UtcNow.AddMinutes(expirationMinute);
            JwtSecurityToken jwtSecurityToken = new(
                audience: configuration["Token:Audience"],
               issuer: configuration["Token:Issuer"],
               expires: token.Expiration,
               notBefore: DateTime.UtcNow, // token üretildiği andan ne kadar süre sonra devreye girsin
               signingCredentials: credentials
               );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
