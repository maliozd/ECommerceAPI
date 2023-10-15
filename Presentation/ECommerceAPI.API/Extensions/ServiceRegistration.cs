using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ECommerceAPI.API.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddConfiguredJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer("Admin", options =>
     {
         options.TokenValidationParameters = new()
         {
             ValidateAudience = true, //Olusturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanacağını belirlediğimiz değer. -> www.random.com
             ValidateIssuer = true, //Olusturulacak token değerini kimin dağıttığını ifade edeceğimiz alan. -> www.myapi.com -> bu proje
             ValidateLifetime = true, //Olusturulan token değerinin süresini kontrol edecek olan doğrulama
             ValidateIssuerSigningKey = true, //Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key verisinin doğrulanması.--> simetrik key -- uygulamaya özel unique key


             ValidAudience = configuration["Token:Audience"],
             ValidIssuer = configuration["Token:Issuer"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),//byte
             LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false, //lifetimeValidator delegatedir. değişkenler temsili
             NameClaimType = ClaimTypes.Name

         };
     });

        }
    }
}
