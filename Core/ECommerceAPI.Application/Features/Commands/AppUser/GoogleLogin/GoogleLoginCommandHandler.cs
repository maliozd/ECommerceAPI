using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Application.Services;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> userManager;
        readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler = null)
        {
            this.userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var validationSettings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "688101953703-m8dc93g4h2ic5ugbhgudkuhr5p6gof06.apps.googleusercontent.com" }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, validationSettings);
            var userInfo = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
            Domain.Entities.Identity.AppUser user = await userManager.FindByLoginAsync(userInfo.LoginProvider, userInfo.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = RenameNewUser.RenameGoogleUser(payload.Name),
                        NameSurname = payload.Name,
                    };
                    var identityResult = await userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
                else
                {
                    return new()
                    {
                        Token = _tokenHandler.CreateJwtToken(5)
                    };
                }
            }

            if (result)
                await userManager.AddLoginAsync(user, userInfo); //aspNetUserLogins tablosuna eklendi
            else
                throw new Exception("Invalid user");

            Token token = _tokenHandler.CreateJwtToken(5);
            return new()
            {
                Token = token
            };

        }
    }
}
