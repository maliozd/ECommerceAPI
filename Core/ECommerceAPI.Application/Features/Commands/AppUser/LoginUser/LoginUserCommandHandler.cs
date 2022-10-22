using ECommerceAPI.Application.Abstraction.Services.Authentication;
using ECommerceAPI.Application.Abstraction.Token;
using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthenticationService _authenticationService; //*

        public LoginUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        //tedesco qwerty
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var tokenResponse = await _authenticationService.LoginAsync(request.UsernameOrEmail, request.Password, 20);
            if (tokenResponse == null)
            {                
                throw new Exception();
            }
            LoginUserSuccessCommandResponse loginUserCommandResponse = new()
            {
                Token = tokenResponse
            };
            return loginUserCommandResponse;
        }

    }
}

