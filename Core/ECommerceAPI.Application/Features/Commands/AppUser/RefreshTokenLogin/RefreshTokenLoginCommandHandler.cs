using ECommerceAPI.Application.Abstraction.Services.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        readonly IAuthenticationService _authenticationService;

        public RefreshTokenLoginCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return new()
                {
                    Token = await _authenticationService.RefreshTokenLoginAsync(request.RefreshToken)
                };
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}



