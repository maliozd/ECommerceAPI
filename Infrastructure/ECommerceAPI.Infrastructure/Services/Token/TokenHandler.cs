using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public Application.Dtos.Token CreateAccessToken(int expirationMinute, AppUser user)
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
               signingCredentials: credentials,
               claims: new List<Claim> { new (ClaimTypes.Name,user.UserName)}
               );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }       

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
