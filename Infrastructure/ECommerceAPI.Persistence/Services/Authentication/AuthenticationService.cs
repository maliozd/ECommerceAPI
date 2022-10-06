using ECommerceAPI.Application.Abstraction.Services.Authentication;
using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Services.Basic;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly IConfiguration _configuration;
        readonly SignInManager<AppUser> _signInManager;

        public AuthenticationService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<Token> GoogleLoginAsync(string idToken, string provider, int tokenLifeTime)
        {

            var validationSettings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginConfigs:Google:ClientId"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);
            var userInfo = new UserLoginInfo(provider, payload.Subject, provider);
            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(userInfo.LoginProvider, userInfo.ProviderKey);
            var result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = NameService.RenameGoogleUser(payload.Name),
                        NameSurname = payload.Name,
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;

                    if (result)
                    {
                        await _userManager.AddLoginAsync(user, userInfo);        //aspNetUserLogins tablosuna eklendi
                        Token token = _tokenHandler.CreateJwtToken(tokenLifeTime);
                        return token;
                    }
                    else
                    {
                        throw new Exception("Invalid user");
                    }
                }
            }

            Token _token = _tokenHandler.CreateJwtToken(tokenLifeTime);
            return _token;
        }
        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int tokenLifeTime)
        {
            var appUser = await _userManager.FindByNameAsync(usernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (appUser == null)
                throw new UserLoginFailedException("Wrong username or email!");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);
            if (signInResult.Succeeded) //authentication ok
            {
                Token token = _tokenHandler.CreateJwtToken(tokenLifeTime);
                return token;
            }
            throw new UserLoginFailedException("Wrong password!");

        }
    }
}


