using ECommerceAPI.Application.Abstraction.Services;
using ECommerceAPI.Application.Abstraction.Services.Authentication;
using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Services.Basic;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        readonly IUserService _userService;
        public AuthenticationService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
            _signInManager = signInManager;
            _userService = userService;
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
                    IdentityResult identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;

                    if (result)
                    {
                        await _userManager.AddLoginAsync(user, userInfo);        //added to aspNetUserLogins  
                        Token token = _tokenHandler.CreateAccessToken(tokenLifeTime,user);
                       await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);
                        return token;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            Token _token = _tokenHandler.CreateAccessToken(tokenLifeTime,user);
            await _userService.UpdateRefreshTokenAsync(_token.RefreshToken, user, _token.Expiration, 10);
            return _token;
        }
        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int tokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
                throw new UserLoginFailedException("Wrong username or email!");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (signInResult.Succeeded) //authentication ok
            {
                Token token = _tokenHandler.CreateAccessToken(tokenLifeTime,user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);

                return token;
            }
            throw new UserLoginFailedException("Wrong password!");
        }
          
        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = _userManager.Users.Where(x => x.RefreshToken == refreshToken).FirstOrDefault();
            if (user == null || user.RefreshTokenExpireDate < DateTime.UtcNow)
            {
                throw new UserLoginFailedException("Invalid Token");
            }
            var token = _tokenHandler.CreateAccessToken(20,user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 20);
            return token;

        }
    }
}


