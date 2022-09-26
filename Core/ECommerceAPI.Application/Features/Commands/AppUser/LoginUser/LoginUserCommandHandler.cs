using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;

        public LoginUserCommandHandler(SignInManager<Domain.Entities.Identity.AppUser> signInManager, UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        //tedesco1234 qwe123
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (appUser == null)
                throw new UserLoginFailedException("Wrong username or email");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);
            if (signInResult.Succeeded) //authentication ok
            {
                //authorizaton                
            }
            return new LoginUserCommandResponse()
            {
                SuccessLogin = true
            };
        }
    }
}
